namespace DA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class serverPhoto1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MS_ServerPhotos", "Photo", c => c.Binary(nullable: false, storeType: "image"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MS_ServerPhotos", "Photo", c => c.Binary(storeType: "image"));
        }
    }
}
