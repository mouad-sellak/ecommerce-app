using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp.Models
{
    public class Offre
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_offre { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]

        public virtual ApplicationUser ApplicationUser { get; set; }

        [Display(Name = "libele", ResourceType = typeof(EcommerceApp.Resources.Views.Offres.Index))]
        public string libele { get; set; }

        [Display(Name = "tauxDeRemise", ResourceType = typeof(EcommerceApp.Resources.Views.Offres.Index))]
        public int taux_remise { get; set; }

        [Display(Name = "dateDexpiration", ResourceType = typeof(EcommerceApp.Resources.Views.Offres.Index))]
        public DateTime date_expiration { get; set; }

        [Display(Name = "dateAjout", ResourceType = typeof(EcommerceApp.Resources.Views.Offres.Index))]
        public DateTime date_ajout { get; set; }

        public virtual ICollection<Product> Products { get; set; }

    }
}