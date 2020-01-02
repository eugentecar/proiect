using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace proiect_pssc.Models
{
    public class Loginnn
    {   
        [Key]
        public string ProductId { get; set; }
        public string Parola { get; set; }
    }
}