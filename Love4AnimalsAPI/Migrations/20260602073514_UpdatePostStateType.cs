using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Love4AnimalsAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePostStateType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
{
       migrationBuilder.Sql("""
         ALTER TABLE "Posts"
         ALTER COLUMN "State" TYPE integer
         USING CASE
            WHEN "State" = 'Active' THEN 0
            WHEN "State" = 'Completed' THEN 1
            WHEN "State" = 'Cancelled' THEN 2
            ELSE 0
         END;
       """);
}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
{
    migrationBuilder.Sql("""
        ALTER TABLE "Posts"
        ALTER COLUMN "State" TYPE text
        USING CASE
            WHEN "State" = 0 THEN 'Active'
            WHEN "State" = 1 THEN 'Completed'
            WHEN "State" = 2 THEN 'Cancelled'
            ELSE 'Active'
        END;
    """);
}
    }
}
