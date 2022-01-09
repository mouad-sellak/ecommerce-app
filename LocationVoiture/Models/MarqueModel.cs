using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp.Models
{
    public class Marque
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_marque { get; set; }

        [Display(Name = "libele", ResourceType =typeof(EcommerceApp.Resources.Models.MarqueModel))]
        public string libele { get; set; }

        [Display(Name = "add_date", ResourceType = typeof(EcommerceApp.Resources.Models.MarqueModel))]
        public DateTime? date_ajout { get; set; }

        public virtual ICollection<Voiture> Voitures { get; set; }
    }
}