using Microsoft.AspNetCore.Mvc;

namespace ArmenianChairDogsitting.API.Tests
{
    public static class ResultMocks
    {
        public static ActionResult GetMock(ResultEnum type)
        {
            switch (type)
            {
                case ResultEnum.Created:
                    return new CreatedResult("qwe", 0);
                case ResultEnum.UnprocessableEntity:
                    return new UnprocessableEntityResult();
                default:
                    throw new ArgumentException();
            }
        }
    }
}
