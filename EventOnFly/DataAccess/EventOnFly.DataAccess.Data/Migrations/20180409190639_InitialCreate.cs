using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace EventOnFly.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DateFrom = table.Column<DateTime>(nullable: false),
                    DateTo = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PropertyValue",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BooleanValue = table.Column<bool>(nullable: true),
                    FloatValue = table.Column<double>(nullable: true),
                    IntegerValue = table.Column<int>(nullable: true),
                    TextValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PropertyValue", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    ServiceType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Property",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DefaultValueId = table.Column<int>(nullable: true),
                    DefaultVaueId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PropertyType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Property", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Property_PropertyValue_DefaultVaueId",
                        column: x => x.DefaultVaueId,
                        principalTable: "PropertyValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ServiceOrder",
                columns: table => new
                {
                    ServiceId = table.Column<int>(nullable: false),
                    BookingId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceOrder", x => new { x.ServiceId, x.BookingId });
                    table.UniqueConstraint("AK_ServiceOrder_BookingId_ServiceId", x => new { x.BookingId, x.ServiceId });
                    table.ForeignKey(
                        name: "FK_ServiceOrder_Booking_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Booking",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceOrder_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRelation",
                columns: table => new
                {
                    Service1Id = table.Column<int>(nullable: false),
                    Service2Id = table.Column<int>(nullable: false),
                    RelationType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRelation", x => new { x.Service1Id, x.Service2Id });
                    table.ForeignKey(
                        name: "FK_ServiceRelation_Service_Service1Id",
                        column: x => x.Service1Id,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ServiceRelation_Service_Service2Id",
                        column: x => x.Service2Id,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTypeRelation",
                columns: table => new
                {
                    ServiceId = table.Column<int>(nullable: false),
                    ServiceType = table.Column<int>(nullable: false),
                    AlowanceScript = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTypeRelation", x => new { x.ServiceId, x.ServiceType });
                    table.ForeignKey(
                        name: "FK_ServiceTypeRelation_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServicePropertyValue",
                columns: table => new
                {
                    ServiceId = table.Column<int>(nullable: false),
                    PropertyId = table.Column<int>(nullable: false),
                    EvaluationScript = table.Column<string>(nullable: true),
                    PropertyValueId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicePropertyValue", x => new { x.ServiceId, x.PropertyId });
                    table.UniqueConstraint("AK_ServicePropertyValue_PropertyId_ServiceId", x => new { x.PropertyId, x.ServiceId });
                    table.ForeignKey(
                        name: "FK_ServicePropertyValue_Property_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServicePropertyValue_PropertyValue_PropertyValueId",
                        column: x => x.PropertyValueId,
                        principalTable: "PropertyValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServicePropertyValue_Service_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Service",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTypePropertyRel",
                columns: table => new
                {
                    ServiceType = table.Column<int>(nullable: false),
                    PropertyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTypePropertyRel", x => new { x.ServiceType, x.PropertyId });
                    table.UniqueConstraint("AK_ServiceTypePropertyRel_PropertyId_ServiceType", x => new { x.PropertyId, x.ServiceType });
                    table.ForeignKey(
                        name: "FK_ServiceTypePropertyRel_Property_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceOrderPropertyValue",
                columns: table => new
                {
                    ServiceId = table.Column<int>(nullable: false),
                    BookingId = table.Column<int>(nullable: false),
                    PropertyId = table.Column<int>(nullable: false),
                    PropertyValueId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceOrderPropertyValue", x => new { x.ServiceId, x.BookingId, x.PropertyId });
                    table.UniqueConstraint("AK_ServiceOrderPropertyValue_BookingId_PropertyId_ServiceId", x => new { x.BookingId, x.PropertyId, x.ServiceId });
                    table.ForeignKey(
                        name: "FK_ServiceOrderPropertyValue_Property_PropertyId",
                        column: x => x.PropertyId,
                        principalTable: "Property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceOrderPropertyValue_PropertyValue_PropertyValueId",
                        column: x => x.PropertyValueId,
                        principalTable: "PropertyValue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ServiceOrderPropertyValue_ServiceOrder_ServiceId_BookingId",
                        columns: x => new { x.ServiceId, x.BookingId },
                        principalTable: "ServiceOrder",
                        principalColumns: new[] { "ServiceId", "BookingId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Property_DefaultVaueId",
                table: "Property",
                column: "DefaultVaueId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceOrderPropertyValue_PropertyId",
                table: "ServiceOrderPropertyValue",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceOrderPropertyValue_PropertyValueId",
                table: "ServiceOrderPropertyValue",
                column: "PropertyValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ServicePropertyValue_PropertyValueId",
                table: "ServicePropertyValue",
                column: "PropertyValueId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRelation_Service2Id",
                table: "ServiceRelation",
                column: "Service2Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceOrderPropertyValue");

            migrationBuilder.DropTable(
                name: "ServicePropertyValue");

            migrationBuilder.DropTable(
                name: "ServiceRelation");

            migrationBuilder.DropTable(
                name: "ServiceTypePropertyRel");

            migrationBuilder.DropTable(
                name: "ServiceTypeRelation");

            migrationBuilder.DropTable(
                name: "ServiceOrder");

            migrationBuilder.DropTable(
                name: "Property");

            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "PropertyValue");
        }
    }
}
