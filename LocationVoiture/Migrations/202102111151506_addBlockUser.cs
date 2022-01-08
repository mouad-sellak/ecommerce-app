namespace LocationVoiture.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addBlockUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "blocked", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "blocked");
        }
    }
}
