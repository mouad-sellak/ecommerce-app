using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocationVoiture.Models
{
    public class Reservation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int id_reservation { get; set; }

        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int id_voiture { get; set; }

        [ForeignKey("id_voiture")]
        public virtual Voiture Voiture { get; set; }

        [Display(Name = "Paiement")]
        public int id_paiement { get; set; }

        [ForeignKey("id_paiement")]
        public virtual Paiement Paiement { get; set; }

        [Display(Name = "date_prise", ResourceType = typeof(Resources.Models.ReservationModel))]
        public DateTime date_prise_en_charge { get; set; }

        [Display(Name = "date_retour", ResourceType = typeof(Resources.Models.ReservationModel))]
        public DateTime date_retour { get; set; }

        [Display(Name = "lien_prise", ResourceType = typeof(Resources.Models.ReservationModel))]
        public string lieu_prise_en_charge { get; set; }

        [Display(Name = "price", ResourceType = typeof(Resources.Models.ReservationModel))]
        public float prix { get; set; }

        [Display(Name = "remarque", ResourceType = typeof(Resources.Models.ReservationModel))]
        [Column(TypeName = "text")]
        public string remarque { get; set; }

        [Display(Name = "date_ajout", ResourceType = typeof(Resources.Models.ReservationModel))]
        public DateTime date_ajout { get; set; } = DateTime.Now;


        public float prix_total(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            double days = (date_retour - date_prise_en_charge).TotalDays + 1;
            Voiture voiture = db.Voitures.Find(id);
            string pt = voiture.Prix_total();
            double prix = Double.Parse(pt);
            float total = float.Parse(days.ToString()) * float.Parse(prix.ToString());
            return total;

        }
    }
}