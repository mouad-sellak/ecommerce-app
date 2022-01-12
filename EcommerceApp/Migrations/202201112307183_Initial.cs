namespace EcommerceApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reclamations", "UserId", "dbo.User");
            DropIndex("dbo.Reclamations", new[] { "UserId" });
            AlterColumn("dbo.Products", "description", c => c.String());
            DropColumn("dbo.Products", "couleur");
            DropTable("dbo.Reclamations");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Reclamations",
                c => new
                    {
                        id_Reclamation = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        description = c.String(unicode: false, storeType: "text"),
                        date_ajout = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id_Reclamation);
            
            AddColumn("dbo.Products", "couleur", c => c.String());
            AlterColumn("dbo.Products", "description", c => c.Int(nullable: false));
            CreateIndex("dbo.Reclamations", "UserId");
            AddForeignKey("dbo.Reclamations", "UserId", "dbo.User", "Id");
        }
    }
}
