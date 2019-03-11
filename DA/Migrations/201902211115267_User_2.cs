namespace DA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class User_2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MS_Users", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MS_Users", "Name");
        }
    }
}
