namespace LibraryManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isReturned_added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IssuedBooks", "IsReturned", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.IssuedBooks", "IsReturned");
        }
    }
}
