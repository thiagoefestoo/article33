using Microsoft.EntityFrameworkCore;
using Artigo33.API.Models;


namespace Artigo33.API.Data;


public class AppDbContext : DbContext
{

    public AppDbContext(
        DbContextOptions<AppDbContext> options
    ) : base(options)
    {

    }



    // ==========================
    // TABELAS PRINCIPAIS
    // ==========================


    public DbSet<User> Users { get; set; } = null!;


    public DbSet<Character> Characters { get; set; } = null!;


    public DbSet<CharacterClass> CharacterClasses { get; set; } = null!;


    public DbSet<Rank> Ranks { get; set; } = null!;


    public DbSet<Mission> Missions { get; set; } = null!;


    public DbSet<CharacterMission> CharacterMissions { get; set; } = null!;


    public DbSet<ShopTransaction> ShopTransactions { get; set; } = null!;



    // ==========================
    // SISTEMA DE COMBATE
    // ==========================


    public DbSet<CombatSession> CombatSessions { get; set; } = null!;



    // ==========================
    // SISTEMA ONLINE UNITY
    // ==========================


    public DbSet<PlayerOnline> PlayersOnline { get; set; } = null!;



    // ==========================
    // INVENTÁRIO RPG
    // ==========================


    public DbSet<Item> Items { get; set; } = null!;


    public DbSet<Inventory> Inventories { get; set; } = null!;


    public DbSet<CharacterInventory> CharacterInventories { get; set; } = null!;



    // ==========================
    // EQUIPAMENTOS RPG
    // ==========================


    public DbSet<CharacterEquipment> CharacterEquipments { get; set; } = null!;



    // ==========================
    // INIMIGOS / COMBATE
    // ==========================


    public DbSet<Enemy> Enemies { get; set; } = null!;


    public DbSet<CombatLog> CombatLogs { get; set; } = null!;



    // ==========================
    // LOOT SYSTEM
    // ==========================


    public DbSet<ItemDrop> ItemDrops { get; set; } = null!;

    // ==============================
    // CONFIGURAÇÕES ENTITY FRAMEWORK
    // ==============================

    protected override void OnModelCreating(
        ModelBuilder modelBuilder
    )
    {
        base.OnModelCreating(modelBuilder);



        // CHARACTER -> CLASS

        modelBuilder.Entity<Character>()
            .HasOne(c => c.CharacterClass)
            .WithMany()
            .HasForeignKey(c => c.CharacterClassId)
            .OnDelete(DeleteBehavior.Restrict);



        // CHARACTER -> RANK

        modelBuilder.Entity<Character>()
            .HasOne(c => c.Rank)
            .WithMany()
            .HasForeignKey(c => c.RankId)
            .OnDelete(DeleteBehavior.Restrict);



        // PLAYER ONLINE -> CHARACTER

        modelBuilder.Entity<PlayerOnline>()
            .HasOne(p => p.Character)
            .WithMany()
            .HasForeignKey(p => p.CharacterId)
            .OnDelete(DeleteBehavior.Cascade);



        // INVENTORY -> CHARACTER

        modelBuilder.Entity<Inventory>()
            .HasOne(i => i.Character)
            .WithOne()
            .HasForeignKey<Inventory>(
                i => i.CharacterId
            )
            .OnDelete(DeleteBehavior.Cascade);



        // CHARACTER INVENTORY

        modelBuilder.Entity<CharacterInventory>()
            .HasOne(ci => ci.Character)
            .WithMany()
            .HasForeignKey(ci => ci.CharacterId)
            .OnDelete(DeleteBehavior.Cascade);



        modelBuilder.Entity<CharacterInventory>()
            .HasOne(ci => ci.Item)
            .WithMany()
            .HasForeignKey(ci => ci.ItemId)
            .OnDelete(DeleteBehavior.Restrict);



        // EQUIPMENT

        modelBuilder.Entity<CharacterEquipment>()
            .HasOne(e => e.Character)
            .WithMany()
            .HasForeignKey(e => e.CharacterId)
            .OnDelete(DeleteBehavior.Cascade);



        modelBuilder.Entity<CharacterEquipment>()
            .HasOne(e => e.Item)
            .WithMany()
            .HasForeignKey(e => e.ItemId)
            .OnDelete(DeleteBehavior.Restrict);



        // COMBAT LOG

        modelBuilder.Entity<CombatLog>()
            .HasOne<Character>()
            .WithMany()
            .HasForeignKey(c => c.CharacterId)
            .OnDelete(DeleteBehavior.Restrict);



        modelBuilder.Entity<CombatLog>()
            .HasOne<Enemy>()
            .WithMany()
            .HasForeignKey(c => c.EnemyId)
            .OnDelete(DeleteBehavior.Restrict);



        // ITEM DROP

        modelBuilder.Entity<ItemDrop>()
            .HasOne(d => d.Enemy)
            .WithMany()
            .HasForeignKey(d => d.EnemyId)
            .OnDelete(DeleteBehavior.Cascade);



        modelBuilder.Entity<ItemDrop>()
            .HasOne(d => d.Item)
            .WithMany()
            .HasForeignKey(d => d.ItemId)
            .OnDelete(DeleteBehavior.Restrict);

    }

}