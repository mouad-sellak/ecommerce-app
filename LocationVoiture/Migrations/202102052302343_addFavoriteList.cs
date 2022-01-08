namespace LocationVoiture.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addFavoriteList : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FavoriteLists",
                c => new
                    {
                        id_favorite = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id_favorite)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FavoriteLists", "UserId", "dbo.User");
            DropIndex("dbo.FavoriteLists", new[] { "UserId" });
            DropTable("dbo.FavoriteLists");
        }
    }
}
