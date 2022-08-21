using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models;

public class UpdateSittingForDayRequest : UpdateOrderRequest
{
    [Range(1, 24)]
    public int VisitQuantity { get; set; }
}
