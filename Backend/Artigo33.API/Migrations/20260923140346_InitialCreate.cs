using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Artigo33.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CharacterClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    InitialHealth = table.Column<int>(type: "integer", nullable: false),
                    InitialMoney = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enemies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Health = table.Column<int>(type: "integer", nullable: false),
                    Attack = table.Column<int>(type: "integer", nullable: false),
                    Defense = table.Column<int>(type: "integer", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    ExperienceReward = table.Column<int>(type: "integer", nullable: false),
                    MoneyReward = table.Column<int>(type: "integer", nullable: false),
                    ReputationReward = table.Column<int>(type: "integer", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enemies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Rarity = table.Column<string>(type: "text", nullable: false),
                    Attack = table.Column<int>(type: "integer", nullable: false),
                    Defense = table.Column<int>(type: "integer", nullable: false),
                    StrengthBonus = table.Column<int>(type: "integer", nullable: false),
                    AgilityBonus = table.Column<int>(type: "integer", nullable: false),
                    IntelligenceBonus = table.Column<int>(type: "integer", nullable: false),
                    VitalityBonus = table.Column<int>(type: "integer", nullable: false),
                    AccuracyBonus = table.Column<int>(type: "integer", nullable: false),
                    CharismaBonus = table.Column<int>(type: "integer", nullable: false),
                    HealthBonus = table.Column<int>(type: "integer", nullable: false),
                    EnergyBonus = table.Column<int>(type: "integer", nullable: false),
                    ReputationBonus = table.Column<int>(type: "integer", nullable: false),
                    EquipmentSlot = table.Column<string>(type: "text", nullable: false),
                    IsEquipment = table.Column<bool>(type: "boolean", nullable: false),
                    BuyPrice = table.Column<int>(type: "integer", nullable: false),
                    SellPrice = table.Column<int>(type: "integer", nullable: false),
                    Weight = table.Column<double>(type: "double precision", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Missions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ExperienceReward = table.Column<int>(type: "integer", nullable: false),
                    MoneyReward = table.Column<int>(type: "integer", nullable: false),
                    ReputationReward = table.Column<int>(type: "integer", nullable: false),
                    RequiredLevel = table.Column<int>(type: "integer", nullable: false),
                    Active = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Missions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ranks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    LevelRequired = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ranks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemDrops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EnemyId = table.Column<int>(type: "integer", nullable: false),
                    ItemId = table.Column<int>(type: "integer", nullable: false),
                    DropChance = table.Column<double>(type: "double precision", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemDrops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemDrops_Enemies_EnemyId",
                        column: x => x.EnemyId,
                        principalTable: "Enemies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemDrops_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Characters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    CharacterClassId = table.Column<int>(type: "integer", nullable: false),
                    RankId = table.Column<int>(type: "integer", nullable: false),
                    SubClass = table.Column<string>(type: "text", nullable: false),
                    Hair = table.Column<string>(type: "text", nullable: false),
                    Skin = table.Column<string>(type: "text", nullable: false),
                    Face = table.Column<string>(type: "text", nullable: false),
                    Faction = table.Column<string>(type: "text", nullable: false),
                    Level = table.Column<int>(type: "integer", nullable: false),
                    Experience = table.Column<int>(type: "integer", nullable: false),
                    AttributePoints = table.Column<int>(type: "integer", nullable: false),
                    Money = table.Column<int>(type: "integer", nullable: false),
                    Health = table.Column<int>(type: "integer", nullable: false),
                    CurrentHealth = table.Column<int>(type: "integer", nullable: false),
                    Energy = table.Column<int>(type: "integer", nullable: false),
                    Strength = table.Column<int>(type: "integer", nullable: false),
                    Agility = table.Column<int>(type: "integer", nullable: false),
                    Intelligence = table.Column<int>(type: "integer", nullable: false),
                    Vitality = table.Column<int>(type: "integer", nullable: false),
                    Accuracy = table.Column<int>(type: "integer", nullable: false),
                    Charisma = table.Column<int>(type: "integer", nullable: false),
                    Reputation = table.Column<int>(type: "integer", nullable: false),
                    CriticalChance = table.Column<int>(type: "integer", nullable: false),
                    DodgeChance = table.Column<int>(type: "integer", nullable: false),
                    MissionsCompleted = table.Column<int>(type: "integer", nullable: false),
                    Arrests = table.Column<int>(type: "integer", nullable: false),
                    CrimesCommitted = table.Column<int>(type: "integer", nullable: false),
                    IsAlive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characters_CharacterClasses_CharacterClassId",
                        column: x => x.CharacterClassId,
                        principalTable: "CharacterClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Characters_Ranks_RankId",
                        column: x => x.RankId,
                        principalTable: "Ranks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CharacterEquipments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CharacterId = table.Column<int>(type: "integer", nullable: false),
                    ItemId = table.Column<int>(type: "integer", nullable: false),
                    Slot = table.Column<string>(type: "text", nullable: false),
                    Equipped = table.Column<bool>(type: "boolean", nullable: false),
                    EquippedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterEquipments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterEquipments_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterEquipments_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CharacterInventories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CharacterId = table.Column<int>(type: "integer", nullable: false),
                    ItemId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Equipped = table.Column<bool>(type: "boolean", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterInventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterInventories_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterInventories_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CharacterMissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CharacterId = table.Column<int>(type: "integer", nullable: false),
                    MissionId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    StartedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RewardClaimed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterMissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CharacterMissions_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterMissions_Missions_MissionId",
                        column: x => x.MissionId,
                        principalTable: "Missions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CombatLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CharacterId = table.Column<int>(type: "integer", nullable: false),
                    EnemyId = table.Column<int>(type: "integer", nullable: false),
                    Result = table.Column<string>(type: "text", nullable: false),
                    DamageDealt = table.Column<int>(type: "integer", nullable: false),
                    DamageReceived = table.Column<int>(type: "integer", nullable: false),
                    Turns = table.Column<int>(type: "integer", nullable: false),
                    CriticalHits = table.Column<int>(type: "integer", nullable: false),
                    Dodges = table.Column<int>(type: "integer", nullable: false),
                    BattleHistory = table.Column<string>(type: "text", nullable: false),
                    ExperienceGained = table.Column<int>(type: "integer", nullable: false),
                    MoneyGained = table.Column<int>(type: "integer", nullable: false),
                    ReputationGained = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombatLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CombatLogs_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CombatLogs_Enemies_EnemyId",
                        column: x => x.EnemyId,
                        principalTable: "Enemies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CombatSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CharacterId = table.Column<int>(type: "integer", nullable: false),
                    EnemyId = table.Column<int>(type: "integer", nullable: false),
                    CharacterHealth = table.Column<int>(type: "integer", nullable: false),
                    EnemyHealth = table.Column<int>(type: "integer", nullable: false),
                    CurrentTurn = table.Column<int>(type: "integer", nullable: false),
                    TurnOwner = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CombatSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CombatSessions_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CombatSessions_Enemies_EnemyId",
                        column: x => x.EnemyId,
                        principalTable: "Enemies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inventories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CharacterId = table.Column<int>(type: "integer", nullable: false),
                    MaxWeight = table.Column<double>(type: "double precision", nullable: false),
                    CurrentWeight = table.Column<double>(type: "double precision", nullable: false),
                    MaxSlots = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Inventories_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlayersOnline",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CharacterId = table.Column<int>(type: "integer", nullable: false),
                    LastHeartbeat = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsOnline = table.Column<bool>(type: "boolean", nullable: false),
                    ConnectedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlayersOnline", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlayersOnline_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShopTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CharacterId = table.Column<int>(type: "integer", nullable: false),
                    ItemId = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopTransactions_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShopTransactions_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterEquipments_CharacterId",
                table: "CharacterEquipments",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterEquipments_ItemId",
                table: "CharacterEquipments",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterInventories_CharacterId",
                table: "CharacterInventories",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterInventories_ItemId",
                table: "CharacterInventories",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterMissions_CharacterId",
                table: "CharacterMissions",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CharacterMissions_MissionId",
                table: "CharacterMissions",
                column: "MissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_CharacterClassId",
                table: "Characters",
                column: "CharacterClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Characters_RankId",
                table: "Characters",
                column: "RankId");

            migrationBuilder.CreateIndex(
                name: "IX_CombatLogs_CharacterId",
                table: "CombatLogs",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CombatLogs_EnemyId",
                table: "CombatLogs",
                column: "EnemyId");

            migrationBuilder.CreateIndex(
                name: "IX_CombatSessions_CharacterId",
                table: "CombatSessions",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_CombatSessions_EnemyId",
                table: "CombatSessions",
                column: "EnemyId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventories_CharacterId",
                table: "Inventories",
                column: "CharacterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemDrops_EnemyId",
                table: "ItemDrops",
                column: "EnemyId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemDrops_ItemId",
                table: "ItemDrops",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_PlayersOnline_CharacterId",
                table: "PlayersOnline",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopTransactions_CharacterId",
                table: "ShopTransactions",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopTransactions_ItemId",
                table: "ShopTransactions",
                column: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterEquipments");

            migrationBuilder.DropTable(
                name: "CharacterInventories");

            migrationBuilder.DropTable(
                name: "CharacterMissions");

            migrationBuilder.DropTable(
                name: "CombatLogs");

            migrationBuilder.DropTable(
                name: "CombatSessions");

            migrationBuilder.DropTable(
                name: "Inventories");

            migrationBuilder.DropTable(
                name: "ItemDrops");

            migrationBuilder.DropTable(
                name: "PlayersOnline");

            migrationBuilder.DropTable(
                name: "ShopTransactions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Missions");

            migrationBuilder.DropTable(
                name: "Enemies");

            migrationBuilder.DropTable(
                name: "Characters");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "CharacterClasses");

            migrationBuilder.DropTable(
                name: "Ranks");
        }
    }
}
