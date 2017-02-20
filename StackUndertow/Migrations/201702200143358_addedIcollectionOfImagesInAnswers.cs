namespace StackUndertow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedIcollectionOfImagesInAnswers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImageUploads", "Answer_Id", c => c.Int());
            CreateIndex("dbo.ImageUploads", "Answer_Id");
            AddForeignKey("dbo.ImageUploads", "Answer_Id", "dbo.Answers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ImageUploads", "Answer_Id", "dbo.Answers");
            DropIndex("dbo.ImageUploads", new[] { "Answer_Id" });
            DropColumn("dbo.ImageUploads", "Answer_Id");
        }
    }
}
