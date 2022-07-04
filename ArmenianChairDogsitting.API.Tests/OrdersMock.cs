using ArmenianChairDogsitting.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ArmenianChairDogsitting.API.Tests
{
    public static class OrdersMock
    {
        public static AbstractOrderRequest GetRequestMock(OrdersEnums type)
        {
            switch (type)
            {
                case OrdersEnums.Created1:
                    return new OrderWalkRequest()
                    {
                        Animals = new List<AnimalRequest>(),
                        IsTrial = false,
                        WalkQuantity = 2,
                        ClientId = 1,
                        Status = Status.Created
                    };
                case OrdersEnums.BadCreated:
                    return new OrderWalkRequest();
                default:
                    throw new ArgumentException();
            }
        }
    }
}
