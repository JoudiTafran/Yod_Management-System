using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace YodMS.Migrations
{
    /// <inheritdoc />
    public partial class InitialSeed1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DocumentTypes",
                columns: new[] { "DocTypeId", "TypeName" },
                values: new object[,]
                {
                    { 1, "تصور" },
                    { 2, "تقرير" },
                    { 3, "بيان" },
                    { 4, "محضر" },
                    { 5, "خطة" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[,]
                {
                    { 1, "رئيس الهيئة التنفيذية" },
                    { 2, "نائب الرئيس" },
                    { 3, "أمين عام" },
                    { 4, "مسؤول مالي" },
                    { 5, "مسؤول علاقات" },
                    { 6, "مسؤول أكاديمي" },
                    { 7, "مسؤول أنشطة" },
                    { 8, "مسؤولة طالبات" },
                    { 9, "مسؤول إعلامي" },
                    { 10, "مٌراقب" }
                });

            migrationBuilder.InsertData(
                table: "RoleDocPerms",
                columns: new[] { "DocTypeId", "RoleId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 1, 3 },
                    { 2, 3 },
                    { 3, 3 },
                    { 4, 3 },
                    { 1, 4 },
                    { 2, 4 },
                    { 1, 5 },
                    { 2, 5 },
                    { 5, 5 },
                    { 1, 6 },
                    { 2, 6 },
                    { 5, 6 },
                    { 1, 7 },
                    { 2, 7 },
                    { 5, 7 },
                    { 1, 8 },
                    { 2, 8 },
                    { 5, 8 },
                    { 1, 9 },
                    { 2, 9 },
                    { 5, 9 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoleDocPerms",
                keyColumns: new[] { "DocTypeId", "RoleId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "RoleDocPerms",
                keyColumns: new[] { "DocTypeId", "RoleId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "RoleDocPerms",
                keyColumns: new[] { "DocTypeId", "RoleId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "RoleDocPerms",
                keyColumns: new[] { "DocTypeId", "RoleId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "RoleDocPerms",
                keyColumns: new[] { "DocTypeId", "RoleId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "RoleDocPerms",
                keyColumns: new[] { "DocTypeId", "RoleId" },
                keyValues: new object[] { 1, 3 });

            migrationBuilder.DeleteData(
                table: "RoleDocPerms",
                keyColumns: new[] { "DocTypeId", "RoleId" },
                keyValues: new object[] { 2, 3 });

            migrationBuilder.DeleteData(
                table: "RoleDocPerms",
                keyColumns: new[] { "DocTypeId", "RoleId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "RoleDocPerms",
                keyColumns: new[] { "DocTypeId", "RoleId" },
                keyValues: new object[] { 4, 3 });

            migrationBuilder.DeleteData(
                table: "RoleDocPerms",
                keyColumns: new[] { "DocTypeId", "RoleId" },
                keyValues: new object[] { 1, 4 });

            migrationBuilder.DeleteData(
                table: "RoleDocPerms",
                keyColumns: new[] { "DocTypeId", "RoleId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "RoleDocPerms",
                keyColumns: new[] { "DocTypeId", "RoleId" },
                keyValues: new object[] { 1, 5 });

            migrationBuilder.DeleteData(
                table: "RoleDocPerms",
                keyColumns: new[] { "DocTypeId", "RoleId" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "RoleDocPerms",
                keyColumns: new[] { "DocTypeId", "RoleId" },
                keyValues: new object[] { 5, 5 });

            migrationBuilder.DeleteData(
                table: "RoleDocPerms",
                keyColumns: new[] { "DocTypeId", "RoleId" },
                keyValues: new object[] { 1, 6 });

            migrationBuilder.DeleteData(
                table: "RoleDocPerms",
                keyColumns: new[] { "DocTypeId", "RoleId" },
                keyValues: new object[] { 2, 6 });

            migrationBuilder.DeleteData(
                table: "RoleDocPerms",
                keyColumns: new[] { "DocTypeId", "RoleId" },
                keyValues: new object[] { 5, 6 });

            migrationBuilder.DeleteData(
                table: "RoleDocPerms",
                keyColumns: new[] { "DocTypeId", "RoleId" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "RoleDocPerms",
                keyColumns: new[] { "DocTypeId", "RoleId" },
                keyValues: new object[] { 2, 7 });

            migrationBuilder.DeleteData(
                table: "RoleDocPerms",
                keyColumns: new[] { "DocTypeId", "RoleId" },
                keyValues: new object[] { 5, 7 });

            migrationBuilder.DeleteData(
                table: "RoleDocPerms",
                keyColumns: new[] { "DocTypeId", "RoleId" },
                keyValues: new object[] { 1, 8 });

            migrationBuilder.DeleteData(
                table: "RoleDocPerms",
                keyColumns: new[] { "DocTypeId", "RoleId" },
                keyValues: new object[] { 2, 8 });

            migrationBuilder.DeleteData(
                table: "RoleDocPerms",
                keyColumns: new[] { "DocTypeId", "RoleId" },
                keyValues: new object[] { 5, 8 });

            migrationBuilder.DeleteData(
                table: "RoleDocPerms",
                keyColumns: new[] { "DocTypeId", "RoleId" },
                keyValues: new object[] { 1, 9 });

            migrationBuilder.DeleteData(
                table: "RoleDocPerms",
                keyColumns: new[] { "DocTypeId", "RoleId" },
                keyValues: new object[] { 2, 9 });

            migrationBuilder.DeleteData(
                table: "RoleDocPerms",
                keyColumns: new[] { "DocTypeId", "RoleId" },
                keyValues: new object[] { 5, 9 });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "DocTypeId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "DocTypeId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "DocTypeId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "DocTypeId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "DocumentTypes",
                keyColumn: "DocTypeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "RoleId",
                keyValue: 9);
        }
    }
}
