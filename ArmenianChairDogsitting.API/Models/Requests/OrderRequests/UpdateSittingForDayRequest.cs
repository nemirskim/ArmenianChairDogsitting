using ArmenianChairDogsitting.API.Extensions;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models;

public class UpdateSittingForDayRequest : UpdateOrderRequest
{
    [Range(Constant.minVisitQuantiy, Constant.maxVisitQuantiy)]
    public int VisitQuantity { get; set; }
}
