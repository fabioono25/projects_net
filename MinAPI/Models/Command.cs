using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MinAPI.Models
{
    public class Command
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? HowTo { get; set; }

        [Required]
        [MaxLength(6)]
        public string? Platform { get; set; }

        [Required]
        public string? CommandLine { get; set; }
    }
}