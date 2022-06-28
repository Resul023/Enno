using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Enno.Models
{
    public class Worker
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(120)]
        [MinLength(10)]
        public string Desc { get; set; }
        [Required]
        [MaxLength(20)]
        [MinLength(5)]
        public string Fullname { get; set; }
        [Required]
        [MaxLength(10)]
        [MinLength(3)]
        public string Position { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
