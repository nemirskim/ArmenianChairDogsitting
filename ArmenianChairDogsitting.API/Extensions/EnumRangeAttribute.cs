using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Extensions;

public class EnumRangeAttribute<T> : ValidationAttribute
{
    private int _minLength;
    private int _maxLength;
    public EnumRangeAttribute()
    {
        _minLength = GetEnumMin<T>();
        _maxLength = GetEnumMax<T>();
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

    private int GetEnumMax<F>()
    {
        return Enum.GetValues(typeof(F)).Cast<int>().Max();
    }

    private int GetEnumMin<F>()
    {
        return Enum.GetValues(typeof(F)).Cast<int>().Min();
    }

}
