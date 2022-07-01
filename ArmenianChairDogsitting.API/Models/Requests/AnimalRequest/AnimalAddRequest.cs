﻿namespace ArmenianChairDogsitting.API.Models;

public class AnimalAddRequest
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Breed { get; set; }
    public int Weight { get; set; }
    public string RecommendationsForCare { get; set; }
}
