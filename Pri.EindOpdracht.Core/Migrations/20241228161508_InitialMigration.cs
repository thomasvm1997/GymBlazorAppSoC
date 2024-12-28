using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Pri.EindOpdracht.Core.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Difficulty = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Achieved = table.Column<bool>(type: "bit", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goals_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Workouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WorkoutTypeId = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    CaloriesBurned = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workouts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Workouts_WorkoutTypes_WorkoutTypeId",
                        column: x => x.WorkoutTypeId,
                        principalTable: "WorkoutTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "Admin", "ADMIN" },
                    { "2", null, "Customer", "CUSTOMER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "BirthDate", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "1", 0, new DateTime(2024, 12, 28, 17, 15, 7, 179, DateTimeKind.Local).AddTicks(4410), "1076089c-eba6-44b3-8ccb-02c34b084bb9", "admin@example.com", false, "Admin", "User", false, null, "ADMIN@EXAMPLE.COM", "ADMIN", "AQAAAAIAAYagAAAAEE+6jh2VpPSn0IE0DRaehLHIjdd3KBL3MxSbc88BC8ShoVIEdz1wUR5Tkfjo/V9ZqQ==", null, false, "adfb7368-29aa-4ee5-ab12-2ba9fec091db", false, "admin" },
                    { "2", 0, new DateTime(2024, 12, 28, 17, 15, 7, 273, DateTimeKind.Local).AddTicks(2610), "fd331aec-d847-47a2-a0d6-a7dd90d9ee63", "customer1@example.com", false, "Customer", "One", false, null, "CUSTOMER1@EXAMPLE.COM", "CUSTOMER1", "AQAAAAIAAYagAAAAEAH4Tv7jE0xIhSPmAMmkBAghAU/bs2uGlpgeP9DCu2xhcAlbqwLqnPUdaDC0pj3vzA==", null, false, "e46b8995-19e8-4eee-8446-0998a7b3f76a", false, "customer1" },
                    { "3", 0, new DateTime(2024, 12, 28, 17, 15, 7, 363, DateTimeKind.Local).AddTicks(1400), "64a72fd1-e15c-4d1c-acff-cc6c92a67fcb", "customer2@example.com", false, "Customer", "Two", false, null, "CUSTOMER2@EXAMPLE.COM", "CUSTOMER2", "AQAAAAIAAYagAAAAEIMjn5ji2FkNLnrrnYh6Bxk1gxsNWBeaMnAHT+urBtAzpsAsh4XByVpO4OMWXOEI3A==", null, false, "7c4e65f6-f4e6-44be-b1b8-85f8c8302b26", false, "customer2" }
                });

            migrationBuilder.InsertData(
                table: "WorkoutTypes",
                columns: new[] { "Id", "Description", "Difficulty", "Name" },
                values: new object[,]
                {
                    { 1, "Boost your heart rate and endurance", "Intermediate", "Cardio" },
                    { 2, "Build muscle and strength", "Advanced", "Strength" },
                    { 3, "Improve range of motion and posture", "Beginner", "Flexibility" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "1", "1" },
                    { "2", "2" },
                    { "2", "3" }
                });

            migrationBuilder.InsertData(
                table: "Goals",
                columns: new[] { "Id", "Achieved", "Description", "TargetDate", "UserId" },
                values: new object[,]
                {
                    { 1, false, "Run 5 km in 30 minutes", new DateTime(2025, 1, 28, 17, 15, 7, 363, DateTimeKind.Local).AddTicks(2576), "2" },
                    { 2, false, "Lose 5 kg in 2 months", new DateTime(2025, 2, 28, 17, 15, 7, 363, DateTimeKind.Local).AddTicks(2584), "2" },
                    { 3, false, "Do 50 push-ups in one go", new DateTime(2025, 3, 28, 17, 15, 7, 363, DateTimeKind.Local).AddTicks(2587), "2" },
                    { 4, false, "Run 10 km in 60 minutes", new DateTime(2025, 1, 28, 17, 15, 7, 363, DateTimeKind.Local).AddTicks(2590), "3" },
                    { 5, false, "Gain 3 kg of muscle in 3 months", new DateTime(2025, 3, 28, 17, 15, 7, 363, DateTimeKind.Local).AddTicks(2593), "3" },
                    { 6, false, "Hold a plank for 5 minutes", new DateTime(2025, 2, 28, 17, 15, 7, 363, DateTimeKind.Local).AddTicks(2597), "3" }
                });

            migrationBuilder.InsertData(
                table: "Workouts",
                columns: new[] { "Id", "CaloriesBurned", "Date", "Duration", "UserId", "WorkoutTypeId" },
                values: new object[,]
                {
                    { 1, 250, new DateTime(2024, 12, 27, 17, 15, 7, 363, DateTimeKind.Local).AddTicks(2448), 30, "2", 1 },
                    { 2, 350, new DateTime(2024, 12, 26, 17, 15, 7, 363, DateTimeKind.Local).AddTicks(2468), 45, "2", 2 },
                    { 3, 100, new DateTime(2024, 12, 25, 17, 15, 7, 363, DateTimeKind.Local).AddTicks(2476), 20, "2", 3 },
                    { 4, 200, new DateTime(2024, 12, 27, 17, 15, 7, 363, DateTimeKind.Local).AddTicks(2479), 25, "3", 1 },
                    { 5, 300, new DateTime(2024, 12, 26, 17, 15, 7, 363, DateTimeKind.Local).AddTicks(2482), 40, "3", 2 },
                    { 6, 80, new DateTime(2024, 12, 25, 17, 15, 7, 363, DateTimeKind.Local).AddTicks(2499), 15, "3", 3 },
                    { 7, 270, new DateTime(2024, 12, 28, 17, 15, 7, 363, DateTimeKind.Local).AddTicks(2503), 35, null, 1 },
                    { 8, 400, new DateTime(2024, 12, 28, 17, 15, 7, 363, DateTimeKind.Local).AddTicks(2506), 50, null, 2 },
                    { 9, 120, new DateTime(2024, 12, 28, 17, 15, 7, 363, DateTimeKind.Local).AddTicks(2508), 25, null, 3 },
                    { 10, 500, new DateTime(2024, 12, 28, 17, 15, 7, 363, DateTimeKind.Local).AddTicks(2516), 60, null, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Goals_UserId",
                table: "Goals",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_UserId",
                table: "Workouts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_WorkoutTypeId",
                table: "Workouts",
                column: "WorkoutTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "Workouts");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "WorkoutTypes");
        }
    }
}
