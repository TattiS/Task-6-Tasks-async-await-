using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Task4WebApp.Migrations
{
    public partial class AddAttributesToPilotStewardess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departures_Plane_PlaneItemId",
                table: "Departures");

            migrationBuilder.DropForeignKey(
                name: "FK_Plane_PlaneTypes_TypeOfPlaneId",
                table: "Plane");

            migrationBuilder.DropForeignKey(
                name: "FK_Stewardesses_Crew_CrewId",
                table: "Stewardesses");

            migrationBuilder.DropIndex(
                name: "IX_Plane_TypeOfPlaneId",
                table: "Plane");

            migrationBuilder.DropColumn(
                name: "DepartureId",
                table: "Stewardesses");

            migrationBuilder.DropColumn(
                name: "TypeOfPlaneId",
                table: "Plane");

            migrationBuilder.AlterColumn<int>(
                name: "CrewId",
                table: "Stewardesses",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Plane",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "PlaneItemId",
                table: "Departures",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Crew",
                columns: new[] { "Id", "PilotId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 }
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "Id", "ArrivalTime", "DeparturePoint", "DepartureTime", "Destination" },
                values: new object[,]
                {
                    { 1, new DateTime(2018, 1, 11, 15, 45, 0, 0, DateTimeKind.Unspecified), "Rome", new DateTime(2018, 1, 11, 15, 45, 0, 0, DateTimeKind.Unspecified), "Paris" },
                    { 2, new DateTime(2018, 1, 11, 15, 45, 0, 0, DateTimeKind.Unspecified), "NY", new DateTime(2018, 1, 11, 15, 45, 0, 0, DateTimeKind.Unspecified), "Paris" },
                    { 3, new DateTime(2018, 1, 11, 15, 45, 0, 0, DateTimeKind.Unspecified), "Ottawa", new DateTime(2018, 1, 11, 15, 45, 0, 0, DateTimeKind.Unspecified), "Paris" },
                    { 4, new DateTime(2018, 1, 11, 15, 45, 0, 0, DateTimeKind.Unspecified), "Rome", new DateTime(2018, 1, 11, 15, 45, 0, 0, DateTimeKind.Unspecified), "Paris" },
                    { 5, new DateTime(2018, 1, 11, 15, 45, 0, 0, DateTimeKind.Unspecified), "NY", new DateTime(2018, 1, 11, 15, 45, 0, 0, DateTimeKind.Unspecified), "Paris" },
                    { 6, new DateTime(2018, 1, 11, 15, 45, 0, 0, DateTimeKind.Unspecified), "Ottawa", new DateTime(2018, 1, 11, 15, 45, 0, 0, DateTimeKind.Unspecified), "Paris" }
                });

            migrationBuilder.InsertData(
                table: "Plane",
                columns: new[] { "Id", "Name", "ReleaseDate", "TimeTicks", "TypeId" },
                values: new object[,]
                {
                    { 1, "Boeing 787", new DateTime(2010, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3786912000000000L, 1 },
                    { 2, "Boeing 737", new DateTime(2013, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4102272000000000L, 2 },
                    { 3, "Boeing 777", new DateTime(2012, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5049216000000000L, 3 }
                });

            migrationBuilder.InsertData(
                table: "Departures",
                columns: new[] { "Id", "CrewItemId", "DepartureDate", "FlightId", "PlaneItemId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2018, 1, 11, 15, 45, 0, 0, DateTimeKind.Unspecified), 2, 3 },
                    { 3, 2, new DateTime(2018, 1, 11, 15, 45, 0, 0, DateTimeKind.Unspecified), 5, 1 },
                    { 2, 3, new DateTime(2018, 2, 13, 11, 30, 0, 0, DateTimeKind.Unspecified), 3, 2 }
                });

            migrationBuilder.UpdateData(
                table: "Stewardesses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CrewId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Stewardesses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CrewId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Stewardesses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CrewId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Stewardesses",
                keyColumn: "Id",
                keyValue: 4,
                column: "CrewId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Stewardesses",
                keyColumn: "Id",
                keyValue: 5,
                column: "CrewId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Stewardesses",
                keyColumn: "Id",
                keyValue: 6,
                column: "CrewId",
                value: 3);

            migrationBuilder.InsertData(
                table: "Ticket",
                columns: new[] { "Id", "FlightId", "Price" },
                values: new object[,]
                {
                    { 19, 6, 45.0 },
                    { 18, 6, 35.0 },
                    { 17, 6, 25.0 },
                    { 16, 5, 45.0 },
                    { 15, 5, 35.0 },
                    { 13, 5, 25.0 },
                    { 12, 4, 45.0 },
                    { 10, 4, 25.0 },
                    { 9, 3, 45.0 },
                    { 7, 3, 25.0 },
                    { 6, 2, 45.0 },
                    { 5, 2, 35.0 },
                    { 4, 2, 25.0 },
                    { 3, 1, 45.0 },
                    { 2, 1, 35.0 },
                    { 1, 1, 25.0 },
                    { 11, 4, 35.0 },
                    { 8, 3, 35.0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Plane_TypeId",
                table: "Plane",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Departures_FlightId",
                table: "Departures",
                column: "FlightId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departures_Flights_FlightId",
                table: "Departures",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Departures_Plane_PlaneItemId",
                table: "Departures",
                column: "PlaneItemId",
                principalTable: "Plane",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Plane_PlaneTypes_TypeId",
                table: "Plane",
                column: "TypeId",
                principalTable: "PlaneTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Stewardesses_Crew_CrewId",
                table: "Stewardesses",
                column: "CrewId",
                principalTable: "Crew",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departures_Flights_FlightId",
                table: "Departures");

            migrationBuilder.DropForeignKey(
                name: "FK_Departures_Plane_PlaneItemId",
                table: "Departures");

            migrationBuilder.DropForeignKey(
                name: "FK_Plane_PlaneTypes_TypeId",
                table: "Plane");

            migrationBuilder.DropForeignKey(
                name: "FK_Stewardesses_Crew_CrewId",
                table: "Stewardesses");

            migrationBuilder.DropIndex(
                name: "IX_Plane_TypeId",
                table: "Plane");

            migrationBuilder.DropIndex(
                name: "IX_Departures_FlightId",
                table: "Departures");

            migrationBuilder.DeleteData(
                table: "Departures",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Departures",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Departures",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Ticket",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Crew",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Crew",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Crew",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Plane",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Plane",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Plane",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Plane");

            migrationBuilder.AlterColumn<int>(
                name: "CrewId",
                table: "Stewardesses",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "DepartureId",
                table: "Stewardesses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TypeOfPlaneId",
                table: "Plane",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PlaneItemId",
                table: "Departures",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.UpdateData(
                table: "Stewardesses",
                keyColumn: "Id",
                keyValue: 1,
                column: "CrewId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Stewardesses",
                keyColumn: "Id",
                keyValue: 2,
                column: "CrewId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Stewardesses",
                keyColumn: "Id",
                keyValue: 3,
                column: "CrewId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Stewardesses",
                keyColumn: "Id",
                keyValue: 4,
                column: "CrewId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Stewardesses",
                keyColumn: "Id",
                keyValue: 5,
                column: "CrewId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Stewardesses",
                keyColumn: "Id",
                keyValue: 6,
                column: "CrewId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_Plane_TypeOfPlaneId",
                table: "Plane",
                column: "TypeOfPlaneId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departures_Plane_PlaneItemId",
                table: "Departures",
                column: "PlaneItemId",
                principalTable: "Plane",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Plane_PlaneTypes_TypeOfPlaneId",
                table: "Plane",
                column: "TypeOfPlaneId",
                principalTable: "PlaneTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Stewardesses_Crew_CrewId",
                table: "Stewardesses",
                column: "CrewId",
                principalTable: "Crew",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
