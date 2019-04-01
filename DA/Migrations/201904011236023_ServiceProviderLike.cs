namespace DA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ServiceProviderLike : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MS_ServiceProviderLikes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ServerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MS_Servers", t => t.ServerId, cascadeDelete: true)
                .ForeignKey("dbo.MS_Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.ServerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MS_ServiceProviderLikes", "UserId", "dbo.MS_Users");
            DropForeignKey("dbo.MS_ServiceProviderLikes", "ServerId", "dbo.MS_Servers");
            DropIndex("dbo.MS_ServiceProviderLikes", new[] { "ServerId" });
            DropIndex("dbo.MS_ServiceProviderLikes", new[] { "UserId" });
            DropTable("dbo.MS_ServiceProviderLikes");
        }
    }
}
