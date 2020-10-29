using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SuppliersMVC.Models
{
    public class SupplierToGroup
    {
        public int SupplierToGroupId { get; set; }
        public int SupplierId { get; set; }
        public int GroupId { get; set; }
        public virtual Supplier Supplier { get; set; }
        public virtual Group Group { get; set; }
    }
}