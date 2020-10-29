using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SuppliersMVC.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        [NotMapped]
        [Display(Name = "Suppliers in Group")]
        public List<Supplier> SuppliersList { get; set; }
        public virtual ICollection<SupplierToGroup> GroupToSuppliers { get; set; }
    }
}