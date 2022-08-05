using ArmenianChairDogsitting.API.Extensions;
using ArmenianChairDogsitting.API.Infrastructure;
using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.API.Models
{
    public class UpdateOrderRequest
    {
        [EnumRange<DistrictEnum>]
        public DistrictEnum District { get; set; }
        [DateTimeRequired]
        public DateTime WorkDate { get; set; }
        [ListLength(1, 4, ErrorMessage = ApiErrorMessage.DogQuantityError)]
        public List<Animal> Animals { get; set; }
    }
}
