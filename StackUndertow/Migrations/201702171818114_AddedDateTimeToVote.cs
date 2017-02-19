namespace StackUndertow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedDateTimeToVote : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Votes", "Created", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Votes", "Created");
        }
    }
}
