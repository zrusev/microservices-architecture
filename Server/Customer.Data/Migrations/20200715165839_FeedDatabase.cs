﻿// <auto-generated />
namespace Customer.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;
    using System;
    using System.IO;
    using System.Reflection;

    public partial class FeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sqlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.sql";
            var sqlPath = Path.Combine(AppContext.BaseDirectory, sqlFile);

            migrationBuilder.Sql(File.ReadAllText(sqlPath));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
