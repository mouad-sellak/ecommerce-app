namespace LocationVoiture.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeId_blackList : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlackLists",
                c => new
                    {
                        id_black = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id_black)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlackLists", "UserId", "dbo.User");
            DropIndex("dbo.BlackLists", new[] { "UserId" });
            DropTable("dbo.BlackLists");
        }
    }
}
