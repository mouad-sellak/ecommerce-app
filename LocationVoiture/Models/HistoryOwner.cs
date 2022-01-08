using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocationVoiture.Models
{
    public class HistoryOwner
    {   
            public List<Reservation> reservations { get; set; }
            public List<Offre> offresDisponibles { get; set; }
            public List<Offre> offresExpires { get; set; }
    }
}