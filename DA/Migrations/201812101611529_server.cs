namespace DA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class server : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MS_Servers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Tel = c.String(maxLength: 100),
                        Address = c.String(maxLength: 2000),
                        IsActive = c.Boolean(nullable: false, defaultValue: true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MS_Servers");
        }
    }
}
