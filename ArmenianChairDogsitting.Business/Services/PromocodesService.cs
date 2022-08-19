using ArmenianChairDogsitting.Data.Repositories;

namespace ArmenianChairDogsitting.Business.Services
{
    public class PromocodesService: IPromocodesService
    {
        IPromocodeRepository _promocodesRepository;

        public PromocodesService(IPromocodeRepository promocodesRepository)
        {
            _promocodesRepository = promocodesRepository;
        }

        public decimal GetDiscount(string promocode)
        {
            var choosenPromocode = _promocodesRepository.GetPromocode(promocode);
            var deafaultDiscount = 1;

            if (choosenPromocode == null)
                return deafaultDiscount;

            if (
                choosenPromocode.StartDate > DateTime.Now ||
                choosenPromocode.EndDate < DateTime.Now)
                return deafaultDiscount;

            return choosenPromocode.Discount;
        }
    }
}
