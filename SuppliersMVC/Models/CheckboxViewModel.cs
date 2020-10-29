using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuppliersMVC.Models
{
    public class CheckboxViewModel
    {
        public int Id { get; set; }
        public string NameOfGroup { get; set; }
        public bool Checked { get; set; }
    }
}