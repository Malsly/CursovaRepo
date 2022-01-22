namespace DAL.Imp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ThemeAndWords",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Theme = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Words",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                        ThemeAndWordsID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ThemeAndWords", t => t.ThemeAndWordsID)
                .Index(t => t.ThemeAndWordsID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Words", "ThemeAndWordsID", "dbo.ThemeAndWords");
            DropIndex("dbo.Words", new[] { "ThemeAndWordsID" });
            DropTable("dbo.Words");
            DropTable("dbo.ThemeAndWords");
        }
    }
}
