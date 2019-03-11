namespace DA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class serverPhoto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MS_ServerPhotos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Photo = c.Binary(storeType: "image"),
                        ServerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MS_Servers", t => t.ServerId, cascadeDelete: true)
                .Index(t => t.ServerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MS_ServerPhotos", "ServerId", "dbo.MS_Servers");
            DropIndex("dbo.MS_ServerPhotos", new[] { "ServerId" });
            DropTable("dbo.MS_ServerPhotos");
        }
    }
}
