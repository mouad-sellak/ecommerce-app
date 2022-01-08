namespace LocationVoiture.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addForeignKeyToOffer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Offres", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Offres", "UserId");
            AddForeignKey("dbo.Offres", "UserId", "dbo.User", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Offres", "UserId", "dbo.User");
            DropIndex("dbo.Offres", new[] { "UserId" });
            DropColumn("dbo.Offres", "UserId");
        }
    }
}
