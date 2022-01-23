namespace LibraryManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changetype_members : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Members", "JoiningDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Members", "JoiningDate", c => c.DateTime(nullable: false));
        }
    }
}
