namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BooksAdd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Books", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Books", "ApplicationUser_Id1", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Karma", c => c.Int(nullable: false));
            CreateIndex("dbo.Books", "ApplicationUser_Id");
            CreateIndex("dbo.Books", "ApplicationUser_Id1");
            AddForeignKey("dbo.Books", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Books", "ApplicationUser_Id1", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "ApplicationUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.Books", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Books", new[] { "ApplicationUser_Id1" });
            DropIndex("dbo.Books", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.AspNetUsers", "Karma");
            DropColumn("dbo.Books", "ApplicationUser_Id1");
            DropColumn("dbo.Books", "ApplicationUser_Id");
        }
    }
}
