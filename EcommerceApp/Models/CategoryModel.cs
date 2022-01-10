using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_category { get; set; }

        [Display(Name = "libele", ResourceType =typeof(EcommerceApp.Resources.Models.CategoryModel))]
        public string libele { get; set; }

        [Display(Name = "add_date", ResourceType = typeof(EcommerceApp.Resources.Models.CategoryModel))]
        public DateTime? date_ajout { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}