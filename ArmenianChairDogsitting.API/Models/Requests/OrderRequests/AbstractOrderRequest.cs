﻿using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models
{
    public class AbstractOrderRequest
    {
        [Required]
        public int ClientId { get; set; }
        [MinLength(1)]
        [MaxLength(4)]
        public List<Animal> Animals { get; set; }
        public Status Status { get; set; }
    }
}
