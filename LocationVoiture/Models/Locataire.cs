using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocationVoiture.Models
{
    public class Locataire
    {
        [Display(Name = "name", ResourceType = typeof(LocationVoiture.Resources.Models.Locataire))]
        public string user_name { get; set; } 
        [Display(Name = "Email")]
        public string email { get; set; }
        [Display(Name = "phone", ResourceType = typeof(LocationVoiture.Resources.Models.Locataire))]
        public string phone { get; set; }
        [Display(Name = "address", ResourceType = typeof(LocationVoiture.Resources.Models.Locataire))]
        public string address { get; set; }
        [Display(Name = "date", ResourceType = typeof(LocationVoiture.Resources.Models.Locataire))]
        public DateTime date_ajout { get; set; }
    }
}