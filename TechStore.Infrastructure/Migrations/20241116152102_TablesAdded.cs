using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TablesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CountOfPurchases = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CPUs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CPUs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DisplayCoverages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisplayCoverages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DisplaySizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisplaySizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DisplayTechnologies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisplayTechnologies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Formats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Formats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RAMs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RAMs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Resolutions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resolutions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sensitivities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Range = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sensitivities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SSDCapacities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SSDCapacities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VideoCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VideoCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SmartWatches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    Warranty = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SellerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartWatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SmartWatches_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SmartWatches_Clients_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SmartWatches_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Headphones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    IsWireless = table.Column<bool>(type: "bit", nullable: false),
                    HasMicrophone = table.Column<bool>(type: "bit", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Warranty = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SellerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Headphones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Headphones_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Headphones_Clients_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Headphones_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Headphones_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Keyboards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsWireless = table.Column<bool>(type: "bit", nullable: false),
                    FormatId = table.Column<int>(type: "int", nullable: true),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Warranty = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SellerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keyboards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Keyboards_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Keyboards_Clients_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Keyboards_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Keyboards_Formats_FormatId",
                        column: x => x.FormatId,
                        principalTable: "Formats",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Keyboards_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Mice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsWireless = table.Column<bool>(type: "bit", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    SensitivityId = table.Column<int>(type: "int", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Warranty = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SellerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mice_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mice_Clients_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Mice_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Mice_Sensitivities_SensitivityId",
                        column: x => x.SensitivityId,
                        principalTable: "Sensitivities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mice_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Televisions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DisplaySizeId = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    DisplayTechnologyId = table.Column<int>(type: "int", nullable: true),
                    ResolutionId = table.Column<int>(type: "int", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Warranty = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SellerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Televisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Televisions_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Televisions_Clients_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Televisions_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Televisions_DisplaySizes_DisplaySizeId",
                        column: x => x.DisplaySizeId,
                        principalTable: "DisplaySizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Televisions_DisplayTechnologies_DisplayTechnologyId",
                        column: x => x.DisplayTechnologyId,
                        principalTable: "DisplayTechnologies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Televisions_Resolutions_ResolutionId",
                        column: x => x.ResolutionId,
                        principalTable: "Resolutions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Televisions_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Laptops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandId = table.Column<int>(type: "int", nullable: false),
                    CPUId = table.Column<int>(type: "int", nullable: false),
                    RAMId = table.Column<int>(type: "int", nullable: false),
                    SSDCapacityId = table.Column<int>(type: "int", nullable: false),
                    VideoCardId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false),
                    DisplaySizeId = table.Column<int>(type: "int", nullable: false),
                    DisplayCoverageId = table.Column<int>(type: "int", nullable: true),
                    DisplayTechnologyId = table.Column<int>(type: "int", nullable: true),
                    ResolutionId = table.Column<int>(type: "int", nullable: true),
                    ColorId = table.Column<int>(type: "int", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Warranty = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    AddedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SellerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Laptops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Laptops_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Laptops_CPUs_CPUId",
                        column: x => x.CPUId,
                        principalTable: "CPUs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Laptops_Clients_SellerId",
                        column: x => x.SellerId,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Laptops_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Laptops_DisplayCoverages_DisplayCoverageId",
                        column: x => x.DisplayCoverageId,
                        principalTable: "DisplayCoverages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Laptops_DisplaySizes_DisplaySizeId",
                        column: x => x.DisplaySizeId,
                        principalTable: "DisplaySizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Laptops_DisplayTechnologies_DisplayTechnologyId",
                        column: x => x.DisplayTechnologyId,
                        principalTable: "DisplayTechnologies",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Laptops_RAMs_RAMId",
                        column: x => x.RAMId,
                        principalTable: "RAMs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Laptops_Resolutions_ResolutionId",
                        column: x => x.ResolutionId,
                        principalTable: "Resolutions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Laptops_SSDCapacities_SSDCapacityId",
                        column: x => x.SSDCapacityId,
                        principalTable: "SSDCapacities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Laptops_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Laptops_VideoCards_VideoCardId",
                        column: x => x.VideoCardId,
                        principalTable: "VideoCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_UserId",
                table: "Clients",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Headphones_BrandId",
                table: "Headphones",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Headphones_ColorId",
                table: "Headphones",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Headphones_SellerId",
                table: "Headphones",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Headphones_TypeId",
                table: "Headphones",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Keyboards_BrandId",
                table: "Keyboards",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Keyboards_ColorId",
                table: "Keyboards",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Keyboards_FormatId",
                table: "Keyboards",
                column: "FormatId");

            migrationBuilder.CreateIndex(
                name: "IX_Keyboards_SellerId",
                table: "Keyboards",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Keyboards_TypeId",
                table: "Keyboards",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Laptops_BrandId",
                table: "Laptops",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Laptops_ColorId",
                table: "Laptops",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Laptops_CPUId",
                table: "Laptops",
                column: "CPUId");

            migrationBuilder.CreateIndex(
                name: "IX_Laptops_DisplayCoverageId",
                table: "Laptops",
                column: "DisplayCoverageId");

            migrationBuilder.CreateIndex(
                name: "IX_Laptops_DisplaySizeId",
                table: "Laptops",
                column: "DisplaySizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Laptops_DisplayTechnologyId",
                table: "Laptops",
                column: "DisplayTechnologyId");

            migrationBuilder.CreateIndex(
                name: "IX_Laptops_RAMId",
                table: "Laptops",
                column: "RAMId");

            migrationBuilder.CreateIndex(
                name: "IX_Laptops_ResolutionId",
                table: "Laptops",
                column: "ResolutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Laptops_SellerId",
                table: "Laptops",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Laptops_SSDCapacityId",
                table: "Laptops",
                column: "SSDCapacityId");

            migrationBuilder.CreateIndex(
                name: "IX_Laptops_TypeId",
                table: "Laptops",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Laptops_VideoCardId",
                table: "Laptops",
                column: "VideoCardId");

            migrationBuilder.CreateIndex(
                name: "IX_Mice_BrandId",
                table: "Mice",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Mice_ColorId",
                table: "Mice",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Mice_SellerId",
                table: "Mice",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Mice_SensitivityId",
                table: "Mice",
                column: "SensitivityId");

            migrationBuilder.CreateIndex(
                name: "IX_Mice_TypeId",
                table: "Mice",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartWatches_BrandId",
                table: "SmartWatches",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartWatches_ColorId",
                table: "SmartWatches",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_SmartWatches_SellerId",
                table: "SmartWatches",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Televisions_BrandId",
                table: "Televisions",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Televisions_ColorId",
                table: "Televisions",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Televisions_DisplaySizeId",
                table: "Televisions",
                column: "DisplaySizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Televisions_DisplayTechnologyId",
                table: "Televisions",
                column: "DisplayTechnologyId");

            migrationBuilder.CreateIndex(
                name: "IX_Televisions_ResolutionId",
                table: "Televisions",
                column: "ResolutionId");

            migrationBuilder.CreateIndex(
                name: "IX_Televisions_SellerId",
                table: "Televisions",
                column: "SellerId");

            migrationBuilder.CreateIndex(
                name: "IX_Televisions_TypeId",
                table: "Televisions",
                column: "TypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Headphones");

            migrationBuilder.DropTable(
                name: "Keyboards");

            migrationBuilder.DropTable(
                name: "Laptops");

            migrationBuilder.DropTable(
                name: "Mice");

            migrationBuilder.DropTable(
                name: "SmartWatches");

            migrationBuilder.DropTable(
                name: "Televisions");

            migrationBuilder.DropTable(
                name: "Formats");

            migrationBuilder.DropTable(
                name: "CPUs");

            migrationBuilder.DropTable(
                name: "DisplayCoverages");

            migrationBuilder.DropTable(
                name: "RAMs");

            migrationBuilder.DropTable(
                name: "SSDCapacities");

            migrationBuilder.DropTable(
                name: "VideoCards");

            migrationBuilder.DropTable(
                name: "Sensitivities");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "DisplaySizes");

            migrationBuilder.DropTable(
                name: "DisplayTechnologies");

            migrationBuilder.DropTable(
                name: "Resolutions");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}
