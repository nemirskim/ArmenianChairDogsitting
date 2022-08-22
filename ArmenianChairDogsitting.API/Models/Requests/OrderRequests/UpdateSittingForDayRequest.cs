using ArmenianChairDogsitting.API.Extensions;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models;

public class UpdateSittingForDayRequest : UpdateOrderRequest
{
    [Range(Constant.MinVisitQuantiy, Constant.MaxVisitQuantiy)]
    public int VisitQuantity { get; set; }
}
