﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shop.Core.Models
{
    [Table("dbo.Suppliers")]
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; }

        [DisplayName("Supplier")]
        public string CompanyName { get; set; }
    }
}