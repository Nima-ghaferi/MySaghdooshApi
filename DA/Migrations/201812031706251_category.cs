namespace DA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class category : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MS_Categories",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50),
                    IsActive = c.Boolean(nullable: false, defaultValue: true),
                })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true);

        }

        public override void Down()
        {
            DropIndex("dbo.MS_Categories", new[] { "Name" });
            DropTable("dbo.MS_Categories");
        }
    }
}
