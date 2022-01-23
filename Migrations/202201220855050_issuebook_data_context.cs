namespace LibraryManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class issuebook_data_context : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IssuedBooks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IssueId = c.String(),
                        IssuedTo = c.String(),
                        BookId = c.Int(nullable: false),
                        Period = c.Int(nullable: false),
                        IssuedDate = c.DateTime(nullable: false),
                        books_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.books_Id)
                .Index(t => t.books_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IssuedBooks", "books_Id", "dbo.Books");
            DropIndex("dbo.IssuedBooks", new[] { "books_Id" });
            DropTable("dbo.IssuedBooks");
        }
    }
}
