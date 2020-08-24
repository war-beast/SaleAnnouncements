using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SaleAnnouncements.DAL.Migrations
{
    public partial class v_01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.CreateTable(
				name: "AspNetRoles",
				columns: table => new
				{
					Id = table.Column<string>(nullable: false),
					Name = table.Column<string>(maxLength: 256, nullable: true),
					NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
					ConcurrencyStamp = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetRoles", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AspNetUsers",
				columns: table => new
				{
					Id = table.Column<string>(nullable: false),
					UserName = table.Column<string>(maxLength: 256, nullable: true),
					NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
					Email = table.Column<string>(maxLength: 256, nullable: true),
					NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
					EmailConfirmed = table.Column<bool>(nullable: false),
					PasswordHash = table.Column<string>(nullable: true),
					SecurityStamp = table.Column<string>(nullable: true),
					ConcurrencyStamp = table.Column<string>(nullable: true),
					PhoneNumber = table.Column<string>(nullable: true),
					PhoneNumberConfirmed = table.Column<bool>(nullable: false),
					TwoFactorEnabled = table.Column<bool>(nullable: false),
					LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
					LockoutEnabled = table.Column<bool>(nullable: false),
					AccessFailedCount = table.Column<int>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_AspNetUsers", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "Categories",
				columns: table => new
				{
					Id = table.Column<Guid>(nullable: false),
					Name = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Categories", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "OfferStatuses",
				columns: table => new
				{
					Id = table.Column<Guid>(nullable: false),
					Name = table.Column<string>(nullable: true),
					Amount = table.Column<long>(nullable: false),
					Price = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_OfferStatuses", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "AspNetRoleClaims",
				columns: table => new
				{
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					RoleId = table.Column<string>(nullable: false),
					ClaimType = table.Column<string>(nullable: true),
					ClaimValue = table.Column<string>(nullable: true)
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
					Id = table.Column<int>(nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					UserId = table.Column<string>(nullable: false),
					ClaimType = table.Column<string>(nullable: true),
					ClaimValue = table.Column<string>(nullable: true)
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
					LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
					ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
					ProviderDisplayName = table.Column<string>(nullable: true),
					UserId = table.Column<string>(nullable: false)
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
					UserId = table.Column<string>(nullable: false),
					RoleId = table.Column<string>(nullable: false)
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
					UserId = table.Column<string>(nullable: false),
					LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
					Name = table.Column<string>(maxLength: 128, nullable: false),
					Value = table.Column<string>(nullable: true)
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
				name: "Messages",
				columns: table => new
				{
					Id = table.Column<Guid>(nullable: false),
					Subject = table.Column<string>(nullable: true),
					Description = table.Column<string>(nullable: true),
					CustomerId = table.Column<string>(nullable: true),
					CompanionId = table.Column<string>(nullable: true),
					ParentId = table.Column<Guid>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Messages", x => x.Id);
					table.ForeignKey(
						name: "FK_Messages_AspNetUsers_CustomerId",
						column: x => x.CustomerId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Offers",
				columns: table => new
				{
					Id = table.Column<Guid>(nullable: false),
					Title = table.Column<string>(nullable: true),
					Description = table.Column<string>(nullable: true),
					CustomerId = table.Column<string>(nullable: true),
					CategoryId = table.Column<Guid>(nullable: false),
					Sort = table.Column<int>(nullable: false),
					CreationDate = table.Column<DateTime>(nullable: false),
					UpdateDate = table.Column<DateTime>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Offers", x => x.Id);
					table.ForeignKey(
						name: "FK_Offers_Categories_CategoryId",
						column: x => x.CategoryId,
						principalTable: "Categories",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_Offers_AspNetUsers_CustomerId",
						column: x => x.CustomerId,
						principalTable: "AspNetUsers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "OffersStatusesMaps",
				columns: table => new
				{
					Id = table.Column<Guid>(nullable: false),
					OfferId = table.Column<Guid>(nullable: false),
					StatusId = table.Column<Guid>(nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_OffersStatusesMaps", x => x.Id);
					table.ForeignKey(
						name: "FK_OffersStatusesMaps_Offers_OfferId",
						column: x => x.OfferId,
						principalTable: "Offers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
					table.ForeignKey(
						name: "FK_OffersStatusesMaps_OfferStatuses_StatusId",
						column: x => x.StatusId,
						principalTable: "OfferStatuses",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
				});

			migrationBuilder.CreateTable(
				name: "Photos",
				columns: table => new
				{
					Id = table.Column<Guid>(nullable: false),
					FileName = table.Column<string>(nullable: true),
					OfferId = table.Column<Guid>(nullable: false),
					Ext = table.Column<string>(nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_Photos", x => x.Id);
					table.ForeignKey(
						name: "FK_Photos_Offers_OfferId",
						column: x => x.OfferId,
						principalTable: "Offers",
						principalColumn: "Id",
						onDelete: ReferentialAction.Cascade);
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
				name: "IX_Messages_CustomerId",
				table: "Messages",
				column: "CustomerId");

			migrationBuilder.CreateIndex(
				name: "IX_Offers_CategoryId",
				table: "Offers",
				column: "CategoryId");

			migrationBuilder.CreateIndex(
				name: "IX_Offers_CustomerId",
				table: "Offers",
				column: "CustomerId");

			migrationBuilder.CreateIndex(
				name: "IX_OffersStatusesMaps_OfferId",
				table: "OffersStatusesMaps",
				column: "OfferId");

			migrationBuilder.CreateIndex(
				name: "IX_OffersStatusesMaps_StatusId",
				table: "OffersStatusesMaps",
				column: "StatusId");

			migrationBuilder.CreateIndex(
				name: "IX_Photos_OfferId",
				table: "Photos",
				column: "OfferId");
		}

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
                name: "Messages");

            migrationBuilder.DropTable(
                name: "OffersStatusesMaps");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "OfferStatuses");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
