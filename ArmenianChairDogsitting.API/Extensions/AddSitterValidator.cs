using ArmenianChairDogsitting.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace ArmenianChairDogsitting.API.Extensions;

public class AddSitterValidator
{
    public static  bool IsOkToAddSitter(SitterRequest sitterRequest)
    {
        if (!(sitterRequest.Age - sitterRequest.Experience >= Constant.minAgeToWork))
            return false;

        return true;
    } 
}
