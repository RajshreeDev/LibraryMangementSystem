namespace LibraryManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changetype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.IssuedBooks", "IssueId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.IssuedBooks", "IssueId", c => c.String());
        }
    }
}
