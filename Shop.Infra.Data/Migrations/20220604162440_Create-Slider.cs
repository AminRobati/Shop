using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Shop.Infra.Data.Migrations
{
    public partial class CreateSlider : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sliders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SliderImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SliderTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SliderText = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Href = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TextBtn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sliders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sliders");
        }
    }
}
