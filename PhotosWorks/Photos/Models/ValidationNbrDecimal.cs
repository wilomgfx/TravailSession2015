using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Photos.Models
{
    public class ValidationNbrDecimal
    {
        public int ValidationNbrDecimalID { get; set; }
        [Required]
        [Range(0, 99.99)]

        public decimal Nb1 { get; set; }
        [Required]
        public decimal Nb2 { get; set; }
    }


}