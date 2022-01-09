namespace EcommerceApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
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
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserType = c.String(),
                        TypeOwner = c.String(),
                        UserAdress = c.String(),
                        blocked = c.Boolean(nullable: false),
                        date_join = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaim",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogin",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);

           /* CreateTable(
                "dbo.Reclamations",
                c => new
                {
                    id_reclamation = c.Int(nullable: false, identity: true),
                    UserId = c.String(maxLength: 128),
                    description = c.String(unicode: false, storeType: "text"),
                    date_ajout = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.id_reclamation)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);*/

            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Voitures",
                c => new
                    {
                        id_voiture = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        id_marque = c.Int(nullable: false),
                        id_offre = c.Int(),
                        matricul = c.String(),
                        nb_passagers = c.Int(nullable: false),
                        couleur = c.String(),
                        prix = c.Single(nullable: false),
                        photo = c.String(),
                        disponible = c.Boolean(nullable: false),
                        anne = c.Int(nullable: false),
                        km = c.String(),
                        date_ajout = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id_voiture)
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.Marques", t => t.id_marque, cascadeDelete: true)
                .ForeignKey("dbo.Offres", t => t.id_offre)
                .Index(t => t.UserId)
                .Index(t => t.id_marque)
                .Index(t => t.id_offre);
            
            CreateTable(
                "dbo.Marques",
                c => new
                    {
                        id_marque = c.Int(nullable: false, identity: true),
                        libele = c.String(),
                        date_ajout = c.DateTime(),
                    })
                .PrimaryKey(t => t.id_marque);
            
            CreateTable(
                "dbo.Offres",
                c => new
                    {
                        id_offre = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        libele = c.String(),
                        taux_remise = c.Int(nullable: false),
                        date_expiration = c.DateTime(nullable: false),
                        date_ajout = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id_offre)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        id_reservation = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        id_voiture = c.Int(nullable: false),
                        id_paiement = c.Int(nullable: false),
                        date_prise_en_charge = c.DateTime(nullable: false),
                        date_retour = c.DateTime(nullable: false),
                        lieu_prise_en_charge = c.String(),
                        prix = c.Single(nullable: false),
                        remarque = c.String(unicode: false, storeType: "text"),
                        date_ajout = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.id_reservation)
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.Paiements", t => t.id_paiement, cascadeDelete: true)
                .ForeignKey("dbo.Voitures", t => t.id_voiture, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.id_voiture)
                .Index(t => t.id_paiement);
            
            CreateTable(
                "dbo.Paiements",
                c => new
                    {
                        id_paiement = c.Int(nullable: false, identity: true),
                        libele = c.String(),
                    })
                .PrimaryKey(t => t.id_paiement);
            
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
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRole", "RoleId", "dbo.Role");
            DropForeignKey("dbo.FavoriteLists", "UserId", "dbo.User");
            DropForeignKey("dbo.BlackLists", "UserId", "dbo.User");
            DropForeignKey("dbo.Reservations", "id_voiture", "dbo.Voitures");
            DropForeignKey("dbo.Reservations", "id_paiement", "dbo.Paiements");
            DropForeignKey("dbo.Reservations", "UserId", "dbo.User");
            DropForeignKey("dbo.Voitures", "id_offre", "dbo.Offres");
            DropForeignKey("dbo.Offres", "UserId", "dbo.User");
            DropForeignKey("dbo.Voitures", "id_marque", "dbo.Marques");
            DropForeignKey("dbo.Voitures", "UserId", "dbo.User");
            DropForeignKey("dbo.UserRole", "UserId", "dbo.User");
            DropForeignKey("dbo.Reclamations", "UserId", "dbo.User");
            DropForeignKey("dbo.UserLogin", "UserId", "dbo.User");
            DropForeignKey("dbo.UserClaim", "UserId", "dbo.User");
            DropIndex("dbo.Role", "RoleNameIndex");
            DropIndex("dbo.FavoriteLists", new[] { "UserId" });
            DropIndex("dbo.Reservations", new[] { "id_paiement" });
            DropIndex("dbo.Reservations", new[] { "id_voiture" });
            DropIndex("dbo.Reservations", new[] { "UserId" });
            DropIndex("dbo.Offres", new[] { "UserId" });
            DropIndex("dbo.Voitures", new[] { "id_offre" });
            DropIndex("dbo.Voitures", new[] { "id_marque" });
            DropIndex("dbo.Voitures", new[] { "UserId" });
            DropIndex("dbo.UserRole", new[] { "RoleId" });
            DropIndex("dbo.UserRole", new[] { "UserId" });
            DropIndex("dbo.Reclamations", new[] { "UserId" });
            DropIndex("dbo.UserLogin", new[] { "UserId" });
            DropIndex("dbo.UserClaim", new[] { "UserId" });
            DropIndex("dbo.User", "UserNameIndex");
            DropIndex("dbo.BlackLists", new[] { "UserId" });
            DropTable("dbo.Role");
            DropTable("dbo.FavoriteLists");
            DropTable("dbo.Paiements");
            DropTable("dbo.Reservations");
            DropTable("dbo.Offres");
            DropTable("dbo.Marques");
            DropTable("dbo.Voitures");
            DropTable("dbo.UserRole");
            DropTable("dbo.Reclamations");
            DropTable("dbo.UserLogin");
            DropTable("dbo.UserClaim");
            DropTable("dbo.User");
            DropTable("dbo.BlackLists");
        }
    }
}
