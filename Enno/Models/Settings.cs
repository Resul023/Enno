using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Enno.Models
{
    public class Settings
    { 
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string Key { get; set; }
        [Required]
        [MaxLength(250)]
        public string Value { get; set; }
    }
}
