using ArmenianChairDogsitting.API.Extensions;
using ArmenianChairDogsitting.API.Infrastructure;
using ArmenianChairDogsitting.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Models
{
    public class UpdateOrderRequest
    {
        [EnumRangeAttribute<District>]
        public District District { get; set; }
        [DateTimeRequired]
        public DateTime WorkDate { get; set; }
        [StringLength(50, MinimumLength = 10)]
        public string Address { get; set; }
        [ListLength(1, 4, ErrorMessage = ApiErrorMessage.DogQuantityError)]
        public List<int> AnimalIds { get; set; }
    }
}
