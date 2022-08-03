using ArmenianChairDogsitting.Data.Entities;
using ArmenianChairDogsitting.Data.Enums;

namespace ArmenianChairDogsitting.Data
{
    public class PropertiesToChangeOrder
    {
        public DistrictEnum District { get; set; }
        public DateTime WorkDate { get; set; }
        public List<Animal> Animals { get; set; }
    }
}
