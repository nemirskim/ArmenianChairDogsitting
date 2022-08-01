﻿using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.Data.Entities;

public class ParamsToSearchSitter
{
    public decimal? PriceMinimum { get; set; }
    public decimal? PriceMaximum { get; set; }
    public int? MinRating { get; set; }
    public bool IsSitterHasComments { get; set; }
    public ServiceEnum ServiceType { get; set; }
    public DistrictEnum District { get; set; }
    
    //породы
}
