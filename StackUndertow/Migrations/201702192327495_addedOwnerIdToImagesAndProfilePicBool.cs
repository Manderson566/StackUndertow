namespace StackUndertow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedOwnerIdToImagesAndProfilePicBool : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImageUploads", "OwnerId", c => c.String(maxLength: 128));
            AddColumn("dbo.ImageUploads", "ProfilePic", c => c.Boolean(nullable: false));
            CreateIndex("dbo.ImageUploads", "OwnerId");
            AddForeignKey("dbo.ImageUploads", "OwnerId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ImageUploads", "OwnerId", "dbo.AspNetUsers");
            DropIndex("dbo.ImageUploads", new[] { "OwnerId" });
            DropColumn("dbo.ImageUploads", "ProfilePic");
            DropColumn("dbo.ImageUploads", "OwnerId");
        }
    }
}
