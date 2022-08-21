﻿using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models;

public class OrderSittingForDayRequest : OrderRequest
{
    [Range(1, 24)]
    public int VisitQuantity { get; set; }
}
