using System;
using System.ComponentModel.DataAnnotations;

namespace DemoWebApp.api.v5
{
    public class SuperVillainDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}