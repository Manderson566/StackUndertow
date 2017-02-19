namespace StackUndertow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedUpandDownVotesToVotecs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Votes", "UpVote", c => c.Int());
            AddColumn("dbo.Votes", "DownVote", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Votes", "DownVote");
            DropColumn("dbo.Votes", "UpVote");
        }
    }
}
