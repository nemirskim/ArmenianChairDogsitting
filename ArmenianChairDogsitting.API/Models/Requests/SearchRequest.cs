using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.API.Models;

public class SearchRequest
{
    public decimal PriceMinimum { get; set; }
    public decimal PriceMaximum { get; set; }
    public int Rating { get; set; }
    public int CommentsQuantity { get; set; }
    public District District { get; set; }
}
