using Microsoft.EntityFrameworkCore;

using Artigo33.API.Data;

using Artigo33.API.Services;





var builder = WebApplication.CreateBuilder(args);









// =====================================
// CONTROLLERS
// =====================================

builder.Services.AddControllers();









// =====================================
// BANCO POSTGRESQL NEON
// =====================================

builder.Services.AddDbContext<AppDbContext>(options =>
{

    options.UseNpgsql(

        builder.Configuration

        .GetConnectionString("DefaultConnection")

    );

});











// =====================================
// SERVICES DO JOGO
// =====================================







// ==========================
// PROGRESSÃO RPG
//
// XP
// LEVEL
// RANK
// ==========================

builder.Services.AddScoped<ProgressionService>();









// ==========================
// MISSÕES
// ==========================

builder.Services.AddScoped<MissionService>();

builder.Services.AddScoped<MissionSeedService>();









// ==========================
// ATRIBUTOS
// ==========================

builder.Services.AddScoped<CharacterAttributeService>();









// ==========================
// INVENTÁRIO
// ==========================

builder.Services.AddScoped<InventoryService>();









// ==========================
// EQUIPAMENTOS
// ==========================

builder.Services.AddScoped<EquipmentService>();









// ==========================
// LOJA RPG
// ==========================

builder.Services.AddScoped<ShopService>();









// ==========================
// COMBATE RPG
//
// Ataque
// Defesa
// Dano
// Vitória
// Derrota
// Recompensa
// ==========================

builder.Services.AddScoped<CombatService>();









// ==========================
// LOOT SYSTEM
//
// Drops
// Itens
// Recompensas
// ==========================

builder.Services.AddScoped<LootService>();









// ==========================
// PERFIL DO JOGADOR
//
// Dados completos
// Unity Ready
// ==========================

builder.Services.AddScoped<ProfileService>();




// ==========================
// UNITY
// ==========================

builder.Services.AddScoped<UnityService>();





// ==========================
// SEEDS
// ==========================



// Inimigos iniciais

builder.Services.AddScoped<EnemySeedService>();





// Itens iniciais

builder.Services.AddScoped<ItemSeedService>();





// Relação inimigo -> item

builder.Services.AddScoped<LootSeedService>();




















// =====================================
// SWAGGER
// =====================================

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

















var app = builder.Build();















// =====================================
// SEEDS DO JOGO
// =====================================

using(var scope = app.Services.CreateScope())

{



    // ==========================
    // MISSÕES
    // ==========================


    var missionSeed =

        scope.ServiceProvider

        .GetRequiredService<MissionSeedService>();


    missionSeed.Seed();








    // ==========================
    // INIMIGOS
    // ==========================


    var enemySeed =

        scope.ServiceProvider

        .GetRequiredService<EnemySeedService>();


    enemySeed.Seed();








    // ==========================
    // ITENS
    // ==========================


    var itemSeed =

        scope.ServiceProvider

        .GetRequiredService<ItemSeedService>();


    itemSeed.Seed();








    // ==========================
    // LOOT
    // ==========================


    var lootSeed =

        scope.ServiceProvider

        .GetRequiredService<LootSeedService>();


    lootSeed.Seed();





}














// =====================================
// AMBIENTE DESENVOLVIMENTO
// =====================================

if(app.Environment.IsDevelopment())

{

    app.UseSwagger();

    app.UseSwaggerUI();

}













// =====================================
// MIDDLEWARES
// =====================================

app.UseHttpsRedirection();


app.MapControllers();







app.Run();