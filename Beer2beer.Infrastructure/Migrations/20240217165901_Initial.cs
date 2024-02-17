using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Beer2beer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Balance = table.Column<decimal>(type: "TEXT", nullable: true),
                    EntryDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    UpdateDate = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    RoleId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Balance", "Email", "EntryDate", "FullName", "UpdateDate" },
                values: new object[,]
                {
                    { 1, null, "Yolanda.Prosacco86@gmail.com", null, "Marcus Durgan", null },
                    { 2, 5611.116408160329208m, "Kenton.Goodwin@yahoo.com", null, "Felicity Weissnat", null },
                    { 3, null, "Hobart.Muller@yahoo.com", null, "Jeanie Bayer", null },
                    { 4, null, "Graciela35@yahoo.com", null, "Gregory Fritsch", null },
                    { 5, null, "Demarco_Gleason@gmail.com", null, "Llewellyn Walsh", null },
                    { 6, null, "Kylee_Cronin@gmail.com", null, "Ernestina Berge", null },
                    { 7, 5993.53897302381475m, "Annette.Conn@hotmail.com", null, "Isobel Reichert", null },
                    { 8, null, "Clement.Hauck83@yahoo.com", null, "Icie Considine", null },
                    { 9, 7755.466163640989023m, "Dangelo.Reichert@yahoo.com", null, "Lorena O'Hara", null },
                    { 10, null, "Brown2@yahoo.com", null, "Geoffrey Jaskolski", null },
                    { 11, 1487.976711079677796m, "Mario.Hamill@gmail.com", null, "Gus Olson", null },
                    { 12, null, "Savanah.Schinner@hotmail.com", null, "Brandon Cassin", null },
                    { 13, 3026.788889546890324m, "Jedidiah.Cruickshank47@hotmail.com", null, "Vince Schmidt", null },
                    { 14, 539.1394957260602434m, "Nellie36@hotmail.com", null, "Bernhard Luettgen", null },
                    { 15, null, "Cassie.Johnston@gmail.com", null, "Halle Veum", null },
                    { 16, null, "Earl_Marvin@gmail.com", null, "Flo Cassin", null },
                    { 17, null, "Dejon_Stiedemann17@gmail.com", null, "Sigrid Maggio", null },
                    { 18, 7924.842370551266533m, "Estella_Mosciski@yahoo.com", null, "Myrtle Rau", null },
                    { 19, 8203.518238216032676m, "Noemi_Weber26@gmail.com", null, "Barrett Considine", null },
                    { 20, null, "Myrtie_Schoen@hotmail.com", null, "Virginie Halvorson", null },
                    { 21, 992.4170413170433663m, "Icie.Schumm53@gmail.com", null, "Karianne Durgan", null },
                    { 22, null, "Sigrid99@gmail.com", null, "Daija Lakin", null },
                    { 23, null, "Aurelia.Heathcote33@yahoo.com", null, "Piper Prohaska", null },
                    { 24, 3815.004489076743049m, "Stephanie_Greenfelder@hotmail.com", null, "Clint Emmerich", null },
                    { 25, 9220.765783472084269m, "Danyka20@gmail.com", null, "Pablo Treutel", null },
                    { 26, null, "Narciso82@hotmail.com", null, "Mark Strosin", null },
                    { 27, null, "Pasquale_McLaughlin90@hotmail.com", null, "Golden Ondricka", null },
                    { 28, 9276.087755858430583m, "Lisette59@hotmail.com", null, "Richie Adams", null },
                    { 29, 1277.599184902921132m, "Lilyan28@hotmail.com", null, "Kathleen Legros", null },
                    { 30, 9284.375511752555323m, "Domenick5@yahoo.com", null, "Arlie Kessler", null },
                    { 31, 5682.877687741686508m, "Valentin_McClure36@hotmail.com", null, "Candido Brown", null },
                    { 32, 8948.884534753300543m, "Teresa_Kulas28@hotmail.com", null, "Skye Buckridge", null },
                    { 33, null, "Hudson.Hyatt@hotmail.com", null, "Lyric Bergstrom", null },
                    { 34, null, "Ceasar.Kiehn@gmail.com", null, "Rosalinda McClure", null },
                    { 35, null, "Emil.Bode@hotmail.com", null, "Cristina Conroy", null },
                    { 36, null, "Mara80@gmail.com", null, "Tyrique Grant", null },
                    { 37, 8634.771846351751726m, "Durward87@hotmail.com", null, "Billy Tremblay", null },
                    { 38, 5157.890491227794902m, "Marc.Murphy@hotmail.com", null, "Edyth Konopelski", null },
                    { 39, null, "Felix.Ledner@gmail.com", null, "Elissa Morar", null },
                    { 40, 4446.573400114418539m, "Marion58@gmail.com", null, "Luz Kshlerin", null },
                    { 41, null, "Darius10@yahoo.com", null, "Mafalda McClure", null },
                    { 42, null, "Alessandro.Dach0@yahoo.com", null, "Aliya Cremin", null },
                    { 43, 5249.640449530623277m, "Retta.Gislason@hotmail.com", null, "Turner Daniel", null },
                    { 44, 3278.600496286258483m, "Bernice.Bosco46@hotmail.com", null, "Delta Farrell", null },
                    { 45, 6933.719154383566687m, "Dasia.Boyle24@gmail.com", null, "Vinnie Ward", null },
                    { 46, null, "Adolfo45@hotmail.com", null, "Tomas Schumm", null },
                    { 47, null, "Crystel_Rutherford@gmail.com", null, "Adelbert Aufderhar", null },
                    { 48, 7980.198994229857075m, "Tillman42@gmail.com", null, "Deondre Reichert", null },
                    { 49, null, "Valentina_Parker@yahoo.com", null, "Daphnee Hahn", null },
                    { 50, 9620.831989452464065m, "Cecil.Mueller@yahoo.com", null, "Stefanie Stracke", null },
                    { 51, null, "Maybelle.Schneider@hotmail.com", null, "Samara Rodriguez", null },
                    { 52, null, "Kiara.Ruecker@yahoo.com", null, "Erling Ernser", null },
                    { 53, 6859.503308821285594m, "Rolando.Kovacek15@gmail.com", null, "Fredrick Kutch", null },
                    { 54, 4063.904398441438561m, "Bulah.Goyette0@gmail.com", null, "Mikel Ryan", null },
                    { 55, 4720.853150983939771m, "Ada.Konopelski@hotmail.com", null, "Rafael Ankunding", null },
                    { 56, null, "Madonna_Romaguera91@yahoo.com", null, "Frederik Yundt", null },
                    { 57, 1661.579683383884032m, "Drew_Aufderhar91@yahoo.com", null, "Helene Kub", null },
                    { 58, null, "Sarina.Daugherty60@yahoo.com", null, "Calista Hudson", null },
                    { 59, 5477.378830178176954m, "Taurean_Doyle@hotmail.com", null, "Jermain Kuhic", null },
                    { 60, null, "Myrna.Zulauf97@hotmail.com", null, "Clementine Dooley", null },
                    { 61, null, "Teagan13@gmail.com", null, "Hazel Pouros", null },
                    { 62, 4262.610675717429463m, "Edwina.Padberg@yahoo.com", null, "Reba Hoeger", null },
                    { 63, null, "Rodrigo3@yahoo.com", null, "Anya Halvorson", null },
                    { 64, null, "Itzel4@yahoo.com", null, "Amber Lemke", null },
                    { 65, 6100.65731445998059m, "Katlynn_Windler@gmail.com", null, "Jeanette Abernathy", null },
                    { 66, null, "Shirley.Quitzon62@hotmail.com", null, "Marion Boyle", null },
                    { 67, 2693.212059945012418m, "Xzavier_Wunsch86@hotmail.com", null, "Kenya Collins", null },
                    { 68, 6527.761797955019104m, "Lewis.Pacocha43@gmail.com", null, "Ray Brekke", null },
                    { 69, 2160.381658335360283m, "Etha_Kshlerin58@gmail.com", null, "Madyson Kautzer", null },
                    { 70, null, "Irwin66@gmail.com", null, "Claudia Parisian", null },
                    { 71, 4743.059600446646797m, "Ella.Stoltenberg@yahoo.com", null, "Winston Konopelski", null },
                    { 72, 849.0507126994957879m, "Ethel.Stracke@yahoo.com", null, "Eladio Jones", null },
                    { 73, null, "Jessika31@yahoo.com", null, "Porter Simonis", null },
                    { 74, 3341.140156740263932m, "Nathanial.Wiza35@yahoo.com", null, "Jaime Feeney", null },
                    { 75, 4222.745809851554578m, "Jacques.Upton11@gmail.com", null, "Jerrold Hintz", null },
                    { 76, null, "Electa36@gmail.com", null, "Efren Morar", null },
                    { 77, 1439.776564817473684m, "Loren_Zulauf68@gmail.com", null, "Dasia Johnston", null },
                    { 78, null, "Ervin2@hotmail.com", null, "Jaime Kutch", null },
                    { 79, null, "Adan.Mante96@yahoo.com", null, "Rosendo Graham", null },
                    { 80, null, "Vincenza48@yahoo.com", null, "Zakary Oberbrunner", null },
                    { 81, 9888.524356646483272m, "Talon15@yahoo.com", null, "Carmine Carter", null },
                    { 82, null, "Kelley.Hettinger94@hotmail.com", null, "Maximilian Block", null },
                    { 83, null, "Kira_Borer@yahoo.com", null, "London Predovic", null },
                    { 84, null, "Sven.Pfannerstill90@hotmail.com", null, "Willow Farrell", null },
                    { 85, 9268.426068530697892m, "Haleigh_Yundt67@gmail.com", null, "Aubree Haag", null },
                    { 86, 4618.44236224613386m, "Percy_Leffler@hotmail.com", null, "Etha Lebsack", null },
                    { 87, null, "Hanna.Blanda87@yahoo.com", null, "Shane Braun", null },
                    { 88, 3837.00160971127639m, "Ena_Hudson@hotmail.com", null, "Nellie Stark", null },
                    { 89, 6450.357932325058168m, "Caroline.Sanford@yahoo.com", null, "Alia Romaguera", null },
                    { 90, 1658.233022844377935m, "Wellington71@yahoo.com", null, "Oma Reilly", null },
                    { 91, 9346.506117384203965m, "Donavon.Lindgren78@yahoo.com", null, "Romaine Waters", null },
                    { 92, 9329.583201885969868m, "Keaton82@hotmail.com", null, "Cyril Mayer", null },
                    { 93, 6639.472867481464042m, "Gus_Harris35@gmail.com", null, "Reva Mayer", null },
                    { 94, 923.1313296651359662m, "Bianka_Reynolds@gmail.com", null, "Ariane Borer", null },
                    { 95, 692.5782271995871219m, "Andy_Baumbach@hotmail.com", null, "Michele Mann", null },
                    { 96, null, "Celia.Dickinson@gmail.com", null, "Federico Beier", null },
                    { 97, null, "Natasha.Durgan@yahoo.com", null, "Austin McCullough", null },
                    { 98, 4048.308125348146219m, "Kirsten_Sanford@hotmail.com", null, "Haylie Leuschke", null },
                    { 99, 2045.353943300905918m, "Nathanial4@gmail.com", null, "Harold Lindgren", null },
                    { 100, 2735.911085498064154m, "Selmer0@gmail.com", null, "Elwyn Gibson", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

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
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true);
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
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
