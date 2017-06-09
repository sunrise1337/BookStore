namespace BookStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userBan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "isBanned", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "isBanned");
        }
    }
}
