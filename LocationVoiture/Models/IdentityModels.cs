using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LocationVoiture.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string UserType { get; set; }
        public string TypeOwner { get; set; }
        public string UserAdress { get; set; }

        public Boolean blocked { get; set; }
        public DateTime date_join { get; set; }
        public virtual ICollection<Reclamation> Reclamations { get; set; }
        public virtual ICollection<Voiture> Voitures { get; set; }


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
    }
       public virtual DbSet<Reclamation> Reclamations { get; set; }
       public virtual DbSet<Marque> Marques { get; set; }
       public virtual DbSet<Offre> Offres { get; set; }
       public virtual DbSet<Paiement> Paiements { get; set; }
       public virtual DbSet<Voiture> Voitures { get; set; }
       public virtual DbSet<Reservation> Reservations { get; set; }
       public virtual DbSet<FavoriteList> FavoriteLists { get; set; }
        public virtual DbSet<BlackList> BlackLists { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // This needs to go before the other rules!


            modelBuilder.Entity<ApplicationUser>().ToTable("User");
            modelBuilder.Entity<IdentityRole>().ToTable("Role");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRole");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaim");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogin");
        }
    }
}