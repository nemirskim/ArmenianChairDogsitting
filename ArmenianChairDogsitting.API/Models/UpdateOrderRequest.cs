using ArmenianChairDogsitting.API.Extensions;
using ArmenianChairDogsitting.API.Infrastructure;
using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.API.Models
{
    public class UpdateOrderRequest
    {
        [EnumRangeAttribute<District>]
        public District District { get; set; }
        [DateTimeRequired]
        public DateTime WorkDate { get; set; }
        [ListLength(1, 4, ErrorMessage = ApiErrorMessage.DogQuantityError)]
        public List<DogRequest> Animals { get; set; }
    }
}
