using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Demo.Models
{
    public class Zip
    {
        [Key]
        public String _id { get; set; }
        [Required]
        public String city { get; set; }
    }
}