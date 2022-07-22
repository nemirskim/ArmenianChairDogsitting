using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Extensions;

public class EnumRangeAttribute : ValidationAttribute
{
    private int _minLength;
    private int _maxLength;
    public EnumRangeAttribute(int min, int max)
    {
        _minLength = min;
        _maxLength = max;
    }

    public override bool IsValid(object value)
    {
        var enumObj = value as ValueType;
        if (enumObj is null
            || (int)enumObj < _minLength
            || (int)enumObj > _maxLength)
        {
            return false;
        }
        return true;
    }
}
