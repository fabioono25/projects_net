using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleAPI.Models
{
    public class Person
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Identifier { get; set; }
    }
}
