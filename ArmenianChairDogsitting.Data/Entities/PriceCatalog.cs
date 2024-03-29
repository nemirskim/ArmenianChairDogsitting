﻿using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.Data.Entities;

public class PriceCatalog
{
    public int Id { get; set; }
    public Service Service { get; set; }
    public decimal Price { get; set; }
    public Sitter Sitter { get; set; }
}
