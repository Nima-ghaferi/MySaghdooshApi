namespace DA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class serverFk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MS_Servers", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.MS_Servers", "CategoryId");
            AddForeignKey("dbo.MS_Servers", "CategoryId", "dbo.MS_Categories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MS_Servers", "CategoryId", "dbo.MS_Categories");
            DropIndex("dbo.MS_Servers", new[] { "CategoryId" });
            DropColumn("dbo.MS_Servers", "CategoryId");
        }
    }
}
