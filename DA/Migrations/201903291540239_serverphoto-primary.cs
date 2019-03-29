namespace DA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class serverphotoprimary : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MS_ServerPhotos", "IsPrimary", c => c.Boolean(nullable: false));
            Sql("CREATE TRIGGER [dbo].[server_photo_deactive_others_insert] ON[dbo].[MS_ServerPhotos] AFTER INSERT AS UPDATE [MS_ServerPhotos] set[IsPrimary] = 0 where [ServerId] = (SELECT [ServerId] FROM INSERTED) AND  [Id] != (SELECT [Id] FROM INSERTED)");
            Sql("CREATE TRIGGER [dbo].[server_photo_deactive_others_update] ON[dbo].[MS_ServerPhotos] AFTER UPDATE AS UPDATE [MS_ServerPhotos] set[IsPrimary] = 0 where [ServerId] = (SELECT [ServerId] FROM INSERTED) AND  [Id] != (SELECT [Id] FROM INSERTED)");

        }
        
        public override void Down()
        {
            DropColumn("dbo.MS_ServerPhotos", "isPrimary");
        }
    }
}
