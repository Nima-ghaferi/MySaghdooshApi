namespace DA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Activation_User : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MS_Activations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ActivationCode = c.String(nullable: false, maxLength: 10),
                        IsActive = c.Boolean(nullable: false, defaultValue: true),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MS_Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            Sql("CREATE TRIGGER [dbo].[activation_deactive_others] ON[dbo].[MS_Activations] AFTER INSERT AS UPDATE[MS_Activations] set[IsActive] = 0 where [UserId] = (SELECT [UserId] FROM INSERTED) AND  [Id] != (SELECT [Id] FROM INSERTED)");

            CreateTable(
                "dbo.MS_Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tel = c.String(nullable: false, maxLength: 11, fixedLength: true),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Tel, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MS_Activations", "UserId", "dbo.MS_Users");
            DropIndex("dbo.MS_Users", new[] { "Tel" });
            DropIndex("dbo.MS_Activations", new[] { "UserId" });
            DropTable("dbo.MS_Users");
            DropTable("dbo.MS_Activations");
        }
    }
}
