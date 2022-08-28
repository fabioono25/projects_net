using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleAPI.Dtos
{
    public class PersonCreateDto
    {
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Identifier { get; set; }
    }
}

