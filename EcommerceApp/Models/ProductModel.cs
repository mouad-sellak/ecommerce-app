using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_product { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "category", ResourceType = typeof(EcommerceApp.Resources.Models.ProductModel))]
        public int id_category { get; set; }

        [ForeignKey("id_category")]
        public virtual Category Category { get; set; }

        [Display(Name = "offre", ResourceType = typeof(EcommerceApp.Resources.Models.ProductModel))]
        public int? id_offre { get; set; }

        [ForeignKey("id_offre")]
        public virtual Offre Offre { get; set; }

        [Display(Name = "title", ResourceType = typeof(EcommerceApp.Resources.Models.ProductModel))]
        public string title { get; set; }

        [Display(Name = "description", ResourceType = typeof(EcommerceApp.Resources.Models.ProductModel))]
        public int description { get; set; }

        [Display(Name = "color", ResourceType = typeof(EcommerceApp.Resources.Models.ProductModel))]
        public string couleur { get; set; }

        [Display(Name = "prix", ResourceType = typeof(EcommerceApp.Resources.Models.ProductModel))]
        public float prix { get; set; }

        [Display(Name = "photo", ResourceType = typeof(EcommerceApp.Resources.Models.ProductModel))]
        public string photo { get; set; }

        [Display(Name = "dispo", ResourceType = typeof(EcommerceApp.Resources.Models.ProductModel))]
        public bool disponible { get; set; }

        [Display(Name = "location", ResourceType = typeof(EcommerceApp.Resources.Models.ProductModel))]
        public string location { get; set; }

        [Display(Name = "add_date", ResourceType = typeof(EcommerceApp.Resources.Models.ProductModel))]
        public DateTime date_ajout { get; set; }


        public string Prix_total()
        {
            if (Offre != null && Offre.date_expiration >= DateTime.Now)
            {
                float price = prix - (prix * Offre.taux_remise) / 100;
                return price.ToString("0.00");
            }

            return prix.ToString("0.00");
        }

    }
}