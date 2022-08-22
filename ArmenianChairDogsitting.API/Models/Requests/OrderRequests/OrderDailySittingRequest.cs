using ArmenianChairDogsitting.API.Extensions;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models;

public class OrderDailySittingRequest : OrderRequest
{
    [Range(Constant.MinDayQuantity, Constant.MaxDayQuantity)]
    public int DayQuantity { get; set; }
    [Range(Constant.MinWalkPerDayQuantity, Constant.MaxWalkPerDayQuantity)]
    public int WalkPerDayQuantity { get; set; }
}
