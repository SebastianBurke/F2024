using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace scbH60Store.Migrations
{
    /// <inheritdoc />
    public partial class AddStockRangesAndEmployeeNotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeNotes",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MaximumStock",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinimumStock",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 1,
                columns: new[] { "EmployeeNotes", "MaximumStock", "MinimumStock" },
                values: new object[] { "In demand", 200, 5 });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 2,
                columns: new[] { "EmployeeNotes", "MaximumStock", "MinimumStock" },
                values: new object[] { "In demand", 200, 5 });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 3,
                columns: new[] { "EmployeeNotes", "MaximumStock", "MinimumStock" },
                values: new object[] { "In demand", 200, 5 });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 4,
                columns: new[] { "EmployeeNotes", "MaximumStock", "MinimumStock" },
                values: new object[] { "In demand", 200, 5 });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 5,
                columns: new[] { "EmployeeNotes", "MaximumStock", "MinimumStock" },
                values: new object[] { "In demand", 200, 5 });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 6,
                columns: new[] { "EmployeeNotes", "MaximumStock", "MinimumStock" },
                values: new object[] { "In demand", 200, 5 });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 7,
                columns: new[] { "EmployeeNotes", "MaximumStock", "MinimumStock" },
                values: new object[] { "In demand", 200, 5 });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 8,
                columns: new[] { "EmployeeNotes", "MaximumStock", "MinimumStock" },
                values: new object[] { "In demand", 200, 5 });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 9,
                columns: new[] { "EmployeeNotes", "MaximumStock", "MinimumStock" },
                values: new object[] { "In demand", 200, 5 });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 10,
                columns: new[] { "EmployeeNotes", "MaximumStock", "MinimumStock" },
                values: new object[] { "In demand", 200, 5 });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 11,
                columns: new[] { "EmployeeNotes", "MaximumStock", "MinimumStock" },
                values: new object[] { "In demand", 200, 5 });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 12,
                columns: new[] { "EmployeeNotes", "MaximumStock", "MinimumStock" },
                values: new object[] { "In demand", 200, 5 });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 13,
                columns: new[] { "EmployeeNotes", "MaximumStock", "MinimumStock" },
                values: new object[] { "In demand", 200, 5 });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 14,
                columns: new[] { "EmployeeNotes", "MaximumStock", "MinimumStock" },
                values: new object[] { "In demand", 200, 5 });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 15,
                columns: new[] { "EmployeeNotes", "MaximumStock", "MinimumStock" },
                values: new object[] { "In demand", 200, 5 });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 16,
                columns: new[] { "EmployeeNotes", "MaximumStock", "MinimumStock" },
                values: new object[] { "In demand", 200, 5 });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 17,
                columns: new[] { "EmployeeNotes", "MaximumStock", "MinimumStock" },
                values: new object[] { "In demand", 200, 5 });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 18,
                columns: new[] { "EmployeeNotes", "MaximumStock", "MinimumStock" },
                values: new object[] { "In demand", 200, 5 });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 19,
                columns: new[] { "EmployeeNotes", "MaximumStock", "MinimumStock" },
                values: new object[] { "In demand", 200, 5 });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "ProductID",
                keyValue: 20,
                columns: new[] { "EmployeeNotes", "MaximumStock", "MinimumStock" },
                values: new object[] { "In demand", 200, 5 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeNotes",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "MaximumStock",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "MinimumStock",
                table: "Product");
        }
    }
}
