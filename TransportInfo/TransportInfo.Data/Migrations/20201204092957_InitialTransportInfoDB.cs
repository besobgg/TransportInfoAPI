using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TransportInfo.Data.Migrations
{
    public partial class InitialTransportInfoDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Color",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColorNameGE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ColorName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Color", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fuel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FuelTypeGE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FuelType = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fuel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonalNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "TransportManufacturer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManufacturerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManufacturerNameGE = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportManufacturer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TransportModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModelNameGE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManufacturerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransportModel_TransportManufacturer_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "TransportManufacturer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transports",
                columns: table => new
                {
                    TransportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VinCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RegistrationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManufactureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManufacturerId = table.Column<int>(type: "int", nullable: false),
                    ModelId = table.Column<int>(type: "int", nullable: false),
                    ColorId = table.Column<int>(type: "int", nullable: false),
                    FuelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transports", x => x.TransportId);
                    table.ForeignKey(
                        name: "FK_Transports_Color_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Color",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transports_Fuel_FuelId",
                        column: x => x.FuelId,
                        principalTable: "Fuel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transports_TransportManufacturer_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "TransportManufacturer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transports_TransportModel_ModelId",
                        column: x => x.ModelId,
                        principalTable: "TransportModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransportPerson",
                columns: table => new
                {
                    TransportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HolderStatus = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportPerson", x => new { x.TransportId, x.PersonId });
                    table.ForeignKey(
                        name: "FK_TransportPerson_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TransportPerson_Transports_TransportId",
                        column: x => x.TransportId,
                        principalTable: "Transports",
                        principalColumn: "TransportId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Color",
                columns: new[] { "Id", "ColorName", "ColorNameGE" },
                values: new object[,]
                {
                    { 10, "silver", "ვერცხლისფერი" },
                    { 11, "green", "მწვანე" },
                    { 12, "red", "წითელი" }
                });

            migrationBuilder.InsertData(
                table: "Fuel",
                columns: new[] { "Id", "FuelType", "FuelTypeGE" },
                values: new object[,]
                {
                    { 10, "Electric", "ელექტრო" },
                    { 11, "Hybrid", "ჰიბრიდი" },
                    { 12, "Petrol", "ბენზინი" },
                    { 13, "Gas/Petrol", "გაზი/ბენზინი" }
                });

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "PersonId", "FirstName", "LastName", "PersonalNumber", "Phone" },
                values: new object[,]
                {
                    { new Guid("08277147-51bb-4b17-bebe-05ad4942436c"), "ბესო", "გოგისვანიძე", "60001137083", "577977329" },
                    { new Guid("d737f21d-e39f-49fb-9b58-98ce7c1386dd"), "ვახო", "კინწურაშვილი", "60001137083", "577977329" },
                    { new Guid("9bad0423-2cf9-4a04-8604-b3d14e0f37eb"), "ლადო", "მშვენიერაძე", "60001137083", "577977329" }
                });

            migrationBuilder.InsertData(
                table: "TransportManufacturer",
                columns: new[] { "Id", "ManufacturerName", "ManufacturerNameGE" },
                values: new object[,]
                {
                    { 10, "MERCEDES-BENZ", "მერსედეს-ბენც" },
                    { 11, "BMW", "ბმვ" },
                    { 12, "AUDI", "აუდი" }
                });

            migrationBuilder.InsertData(
                table: "TransportModel",
                columns: new[] { "Id", "ManufacturerId", "ModelName", "ModelNameGE" },
                values: new object[] { 10, 10, "E200", "ე200" });

            migrationBuilder.InsertData(
                table: "TransportModel",
                columns: new[] { "Id", "ManufacturerId", "ModelName", "ModelNameGE" },
                values: new object[] { 11, 11, "325", "325" });

            migrationBuilder.InsertData(
                table: "TransportModel",
                columns: new[] { "Id", "ManufacturerId", "ModelName", "ModelNameGE" },
                values: new object[] { 12, 12, "A8", "ა8" });

            migrationBuilder.InsertData(
                table: "Transports",
                columns: new[] { "TransportId", "ColorId", "FuelId", "ManufactureDate", "ManufacturerId", "ModelId", "Note", "Photo", "RegistrationNumber", "VinCode" },
                values: new object[] { new Guid("08277147-51bb-4b17-bebe-05ad4942435c"), 10, 10, new DateTime(1990, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 10, null, null, "AA-200-AA", "34353453THFDHJ87Y6" });

            migrationBuilder.InsertData(
                table: "Transports",
                columns: new[] { "TransportId", "ColorId", "FuelId", "ManufactureDate", "ManufacturerId", "ModelId", "Note", "Photo", "RegistrationNumber", "VinCode" },
                values: new object[] { new Guid("08277147-51bb-4b17-bebe-05ad4942445c"), 11, 12, new DateTime(2000, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, 11, null, null, "BB-200-AA", "34353453THFDHJ87Y6" });

            migrationBuilder.InsertData(
                table: "Transports",
                columns: new[] { "TransportId", "ColorId", "FuelId", "ManufactureDate", "ManufacturerId", "ModelId", "Note", "Photo", "RegistrationNumber", "VinCode" },
                values: new object[] { new Guid("08277147-51bb-4b17-bebe-05ad4943435c"), 12, 13, new DateTime(1999, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, 11, null, null, "CC-200-AA", "34353453THFDHJ87Y6" });

            migrationBuilder.InsertData(
                table: "TransportPerson",
                columns: new[] { "PersonId", "TransportId", "HolderStatus" },
                values: new object[] { new Guid("08277147-51bb-4b17-bebe-05ad4942436c"), new Guid("08277147-51bb-4b17-bebe-05ad4942435c"), true });

            migrationBuilder.InsertData(
                table: "TransportPerson",
                columns: new[] { "PersonId", "TransportId", "HolderStatus" },
                values: new object[] { new Guid("d737f21d-e39f-49fb-9b58-98ce7c1386dd"), new Guid("08277147-51bb-4b17-bebe-05ad4942435c"), false });

            migrationBuilder.InsertData(
                table: "TransportPerson",
                columns: new[] { "PersonId", "TransportId", "HolderStatus" },
                values: new object[] { new Guid("d737f21d-e39f-49fb-9b58-98ce7c1386dd"), new Guid("08277147-51bb-4b17-bebe-05ad4942445c"), true });

            migrationBuilder.CreateIndex(
                name: "IX_TransportModel_ManufacturerId",
                table: "TransportModel",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_TransportPerson_PersonId",
                table: "TransportPerson",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Transports_ColorId",
                table: "Transports",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Transports_FuelId",
                table: "Transports",
                column: "FuelId");

            migrationBuilder.CreateIndex(
                name: "IX_Transports_ManufacturerId",
                table: "Transports",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Transports_ModelId",
                table: "Transports",
                column: "ModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransportPerson");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Transports");

            migrationBuilder.DropTable(
                name: "Color");

            migrationBuilder.DropTable(
                name: "Fuel");

            migrationBuilder.DropTable(
                name: "TransportModel");

            migrationBuilder.DropTable(
                name: "TransportManufacturer");
        }
    }
}
