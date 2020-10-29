using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SuppliersMVC.Models
{
    public class SuppliersViewModel
    {
        public int SupplierId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Address { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [RegularExpression(@"^\d{9,9}$", ErrorMessage = "Phone Number must be a 9 digit number with no spaces.")]
        public string PhoneNumber { get; set; }
        public List<CheckboxViewModel> Groups { get; set; }
    }
}