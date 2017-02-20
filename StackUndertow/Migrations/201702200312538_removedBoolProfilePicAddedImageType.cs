namespace StackUndertow.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedBoolProfilePicAddedImageType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ImageUploads", "ImgType", c => c.String());
            DropColumn("dbo.ImageUploads", "ProfilePic");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ImageUploads", "ProfilePic", c => c.Boolean(nullable: false));
            DropColumn("dbo.ImageUploads", "ImgType");
        }
    }
}
