
using Microsoft.EntityFrameworkCore;
using Artigo33.API.Data;
using Artigo33.API.Services;

var builder = WebApplication.CreateBuilder(args);

// =====================================
// CONFIGURAÇÃO DO BANCO POSTGRESQL NEON
// =====================================

string connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "[ARTIGO33] ConnectionStrings:DefaultConnection não foi configurada. " +
        "Configure a conexão Neon no ambiente de execução."
    );

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "[ARTIGO33] A string de conexão PostgreSQL está vazia."
    );
}

// Nunca registrar a connection string em logs.
// Ela contém credenciais sensíveis.

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connectionString, npgsqlOptions =>
    {
        npgsqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(10),
            errorCodesToAdd: null
        );

        npgsqlOptions.CommandTimeout(30);
    });
});

// =====================================
// CONTROLLERS
// =====================================

builder.Services.AddControllers();

// =====================================
// SERVICES DO JOGO
// =====================================

// PROGRESSÃO RPG
// XP / LEVEL / RANK
builder.Services.AddScoped<ProgressionService>();

// MISSÕES
builder.Services.AddScoped<MissionService>();
builder.Services.AddScoped<MissionSeedService>();

// ATRIBUTOS
builder.Services.AddScoped<CharacterAttributeService>();

// INVENTÁRIO
builder.Services.AddScoped<InventoryService>();

// EQUIPAMENTOS
builder.Services.AddScoped<EquipmentService>();

// LOJA RPG
builder.Services.AddScoped<ShopService>();

// COMBATE RPG
// Ataque / Defesa / Dano / Recompensa
builder.Services.AddScoped<CombatService>();

// LOOT SYSTEM
// Drops / Itens / Recompensas
builder.Services.AddScoped<LootService>();

// PERFIL DO JOGADOR
builder.Services.AddScoped<ProfileService>();

// INTEGRAÇÃO UNITY
builder.Services.AddScoped<UnityService>();

// =====================================
// SEEDS DO JOGO
// =====================================

// Inimigos iniciais
builder.Services.AddScoped<EnemySeedService>();

// Itens iniciais
builder.Services.AddScoped<ItemSeedService>();

// Relação inimigo -> item
builder.Services.AddScoped<LootSeedService>();

// =====================================
// SWAGGER / OPENAPI
// =====================================

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// =====================================
// CONSTRUIR A APLICAÇÃO
// =====================================

var app = builder.Build();

app.Logger.LogInformation(
    "[ARTIGO33] Inicializando API. Ambiente: {Environment}",
    app.Environment.EnvironmentName
);

// =====================================
// SEEDS DO JOGO
// =====================================

// Em desenvolvimento, mantém o comportamento
// anterior: executar os seeds na inicialização.
//
// Em produção, a execução automática é
// desabilitada por segurança, evitando
// alterações inesperadas no banco Neon.
//
// Para uma execução planejada em produção,
// a opção Game:RunSeeds pode ser configurada
// explicitamente como true.

bool runSeeds = app.Environment.IsDevelopment()
    || app.Configuration.GetValue<bool>("Game:RunSeeds");

if (runSeeds)
{
    app.Logger.LogInformation(
        "[ARTIGO33] Iniciando seeds do jogo."
    );

    try
    {
        using var scope = app.Services.CreateScope();

        // MISSÕES
        var missionSeed = scope.ServiceProvider
            .GetRequiredService<MissionSeedService>();

        missionSeed.Seed();

        // INIMIGOS
        var enemySeed = scope.ServiceProvider
            .GetRequiredService<EnemySeedService>();

        enemySeed.Seed();

        // ITENS
        var itemSeed = scope.ServiceProvider
            .GetRequiredService<ItemSeedService>();

        itemSeed.Seed();

        // LOOT
        var lootSeed = scope.ServiceProvider
            .GetRequiredService<LootSeedService>();

        lootSeed.Seed();

        app.Logger.LogInformation(
            "[ARTIGO33] Seeds finalizados."
        );
    }
    catch (Exception ex)
    {
        app.Logger.LogCritical(
            ex,
            "[ARTIGO33] Falha na execução dos seeds."
        );

        throw;
    }
}
else
{
    app.Logger.LogInformation(
        "[ARTIGO33] Seeds automáticos desabilitados neste ambiente."
    );
}

// =====================================
// SWAGGER - DESENVOLVIMENTO
// =====================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// =====================================
// MIDDLEWARES
// =====================================

// HTTPS será terminado pelo Nginx na AWS.
// A configuração segura dos cabeçalhos do
// proxy será feita na etapa do Nginx.

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

// =====================================
// ROTAS DA API
// =====================================

app.MapControllers();

// =====================================
// INICIAR API
// =====================================

app.Logger.LogInformation(
    "[ARTIGO33] API pronta para iniciar."
);

app.Run();
