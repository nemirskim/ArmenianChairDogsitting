using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models;

public class OrderWalkRequest : OrderRequest
{
    public bool IsTrial { get; set; }
}
