using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.Data
{
    public class UpdateOrderModel
    {
        public Enums.District District { get; set; }
        public DateTime WorkDate { get; set; }
        public string Address { get; set; }
        public List<Animal> Animals { get; set; }
    }
}
