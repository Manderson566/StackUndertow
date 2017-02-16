namespace StackUndertow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedQAnswertoAnswerModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "QAnswer", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Answers", "QAnswer");
        }
    }
}
