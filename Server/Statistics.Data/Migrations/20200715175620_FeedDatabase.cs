﻿// <auto-generated />
namespace Statistics.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;
    using System;
    using System.IO;
    using System.Reflection;

    public partial class FeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "BoughtProducts",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");


            var sqlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.sql";
            var sqlPath = Path.Combine(AppContext.BaseDirectory, sqlFile);

            migrationBuilder.Sql(File.ReadAllText(sqlPath));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "BoughtProducts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
