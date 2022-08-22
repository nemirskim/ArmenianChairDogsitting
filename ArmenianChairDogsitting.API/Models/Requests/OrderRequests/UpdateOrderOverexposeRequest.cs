using ArmenianChairDogsitting.API.Extensions;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models;

public class UpdateOrderOverexposeRequest : UpdateOrderRequest 
{
    [Range(Constant.MinDayQuantity, Constant.MaxDayQuantity)]
    public int DayQuantity { get; set; }
    [Required]
    public int WalkPerDayQuantity { get; set; }
}
