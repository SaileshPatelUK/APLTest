using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APLTechnical.Infrastructure.Migrations;

/// <inheritdoc />
public partial class InitialMig001 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Images",
            columns: table => new
            {
                Id = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                OriginalFileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                BlobNameOrPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                ContentType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                SizeBytes = table.Column<long>(type: "bigint", nullable: false),
                Width = table.Column<int>(type: "int", nullable: false),
                Height = table.Column<int>(type: "int", nullable: false),
                StorageLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                UploadedAtUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                UploadedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Images", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Images");
    }
}
