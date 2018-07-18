using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Task4WebApp.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departures_Crew_CrewItemId",
                table: "Departures");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Stewardesses",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Stewardesses",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartureId",
                table: "Stewardesses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "PlaneTypes",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Plane",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Pilots",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Pilots",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Destination",
                table: "Flights",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DeparturePoint",
                table: "Flights",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CrewItemId",
                table: "Departures",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Pilots",
                columns: new[] { "Id", "BirthDate", "Name", "Surname", "TimeTicks" },
                values: new object[,]
                {
                    { 1, new DateTime(1978, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Adam", "Benn", 4298400000000000L },
                    { 2, new DateTime(1964, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Max", "Payne", 9881568000000000L },
                    { 3, new DateTime(1974, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Francis", "Castle", 8271072000000000L }
                });

            migrationBuilder.InsertData(
                table: "PlaneTypes",
                columns: new[] { "Id", "AirLift", "Model", "Seats" },
                values: new object[,]
                {
                    { 1, 58100, "Boeing 737-200", 130 },
                    { 2, 244900, "Boeing 787 Dreamliner", 250 },
                    { 3, 351800, "Boeing 777-300", 550 }
                });

            migrationBuilder.InsertData(
                table: "Stewardesses",
                columns: new[] { "Id", "BirthDate", "CrewId", "DepartureId", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, new DateTime(2001, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "Eve", "Fairy" },
                    { 2, new DateTime(1999, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "Samantha", "Simson" },
                    { 3, new DateTime(1998, 5, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "Gabrial", "Fate" },
                    { 4, new DateTime(1993, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "Hannah", "Screw" },
                    { 5, new DateTime(1996, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "Jennah", "Johns" },
                    { 6, new DateTime(1992, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, "Ivory", "Rocket" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Departures_Crew_CrewItemId",
                table: "Departures",
                column: "CrewItemId",
                principalTable: "Crew",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departures_Crew_CrewItemId",
                table: "Departures");

            migrationBuilder.DeleteData(
                table: "Pilots",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pilots",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Pilots",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PlaneTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PlaneTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PlaneTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Stewardesses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Stewardesses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Stewardesses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Stewardesses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Stewardesses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Stewardesses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DropColumn(
                name: "DepartureId",
                table: "Stewardesses");

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Stewardesses",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Stewardesses",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "PlaneTypes",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Plane",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Surname",
                table: "Pilots",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Pilots",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Destination",
                table: "Flights",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "DeparturePoint",
                table: "Flights",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<int>(
                name: "CrewItemId",
                table: "Departures",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Departures_Crew_CrewItemId",
                table: "Departures",
                column: "CrewItemId",
                principalTable: "Crew",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
