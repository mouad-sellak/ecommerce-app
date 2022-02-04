using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcommerceApp.Models
{
    public class HistoryOwner
    {
            public List<Product> productsDisponibles { get; set; }
            public List<Offre> offresDisponibles { get; set; }
            public List<Offre> offresExpires { get; set; }
    }
}