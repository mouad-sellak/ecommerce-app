namespace LocationVoiture.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnToUserType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "UserType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "UserType");
        }
    }
}
