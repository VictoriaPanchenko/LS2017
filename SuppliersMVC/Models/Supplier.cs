using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SuppliersMVC.Models
{
    public class Supplier
    {
        public int SupplierId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [RegularExpression(@"^\d{9,9}$", ErrorMessage = "Phone Number must be a 9 digit number with no spaces.")]
        public string PhoneNumber { get; set; }
        public virtual ICollection<SupplierToGroup> SupplierToGroups { get; set; }
    }
}