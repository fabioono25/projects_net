using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MinAPI.Dtos
{
    public class CommandUpdateDto
    {
        [Required]
        public string? HowTo { get; set; }

        [Required]
        [MaxLength(5)]
        public string? Platform { get; set; }

        [Required]
        public string? CommandLine { get; set; }
    }
}