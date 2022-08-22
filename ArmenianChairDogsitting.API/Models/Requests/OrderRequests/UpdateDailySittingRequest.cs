using ArmenianChairDogsitting.API.Extensions;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models;

public class UpdateDailySittingRequest : UpdateOrderRequest
{
    [Range(Constant.minDayQuantity, Constant.maxDayQuantity)]
    public int DayQuantity { get; set; }
    [Range(Constant.minWalkPerDayQuantity, Constant.maxWalkPerDayQuantity)]
    public int WalkPerDayQuantity { get; set; }
}
