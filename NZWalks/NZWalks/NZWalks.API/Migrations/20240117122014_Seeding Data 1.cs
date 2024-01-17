using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalks.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingData1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("460c85b4-6cd2-4807-98cd-29b58b865686"), "Easy" },
                    { new Guid("71f69bb3-9acd-47c9-b4ad-64f30615aec3"), "Medium" },
                    { new Guid("9e05d345-4857-4ebc-81a6-9089d1f03c0f"), "Hard" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("2e59b289-3d9a-4b29-9c7c-b7f111a5e21b"), "WLG", "Wellington", "WLG.png" },
                    { new Guid("4807e3d9-0472-4ec2-b7ae-bdfa567b9769"), "Bay of Plenty", "BOP", "BOP.png" },
                    { new Guid("7e1ff06e-7cf7-40e4-b6f3-6c9a11ec6540"), "AUK", "Aukland", "AKL.png" },
                    { new Guid("d3964401-5fdc-4aa9-9cfd-70a37a513a5b"), "NLS", "Nelson", "NLS.png" },
                    { new Guid("fa85a572-b6ed-48fd-afa9-41438656e220"), "Southland", "STL", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("460c85b4-6cd2-4807-98cd-29b58b865686"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("71f69bb3-9acd-47c9-b4ad-64f30615aec3"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("9e05d345-4857-4ebc-81a6-9089d1f03c0f"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("2e59b289-3d9a-4b29-9c7c-b7f111a5e21b"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("4807e3d9-0472-4ec2-b7ae-bdfa567b9769"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("7e1ff06e-7cf7-40e4-b6f3-6c9a11ec6540"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("d3964401-5fdc-4aa9-9cfd-70a37a513a5b"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("fa85a572-b6ed-48fd-afa9-41438656e220"));
        }
    }
}
