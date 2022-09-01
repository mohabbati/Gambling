using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gambling.Data.Migrations
{
    public partial class AddSequentialGuidForIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Plays",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NewSequentialID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NewSequentialID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "AspNetRoles",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NewSequentialID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "AccountTransactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NewSequentialID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Accounts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NewSequentialID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Plays",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NewSequentialID()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NewSequentialID()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "AspNetRoles",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NewSequentialID()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "AccountTransactions",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NewSequentialID()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Accounts",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NewSequentialID()");
        }
    }
}
