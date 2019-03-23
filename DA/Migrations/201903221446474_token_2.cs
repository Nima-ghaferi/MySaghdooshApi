namespace DA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class token_2 : DbMigration
    {
        public override void Up()
        {
            Sql("CREATE TRIGGER [dbo].[token_deactive_others] ON[dbo].[MS_Tokens] AFTER INSERT AS UPDATE[MS_Tokens] set[IsActive] = 0 where [UserId] = (SELECT [UserId] FROM INSERTED) AND  [Id] != (SELECT [Id] FROM INSERTED)");
        }
        
        public override void Down()
        {
        }
    }
}
