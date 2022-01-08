using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace LocationVoiture.Models
{
    public class Paiement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_paiement { get; set; }

        [Display(Name = "Libele")]
        public string libele { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }

    }
}