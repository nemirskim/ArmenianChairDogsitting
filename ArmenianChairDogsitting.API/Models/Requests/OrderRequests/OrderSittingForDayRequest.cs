using ArmenianChairDogsitting.API.Extensions;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models;

public class OrderSittingForDayRequest : OrderRequest
{
    [Range(Constant.MinVisitQuantiy, Constant.MaxVisitQuantiy)]
    public int VisitQuantity { get; set; }
}
