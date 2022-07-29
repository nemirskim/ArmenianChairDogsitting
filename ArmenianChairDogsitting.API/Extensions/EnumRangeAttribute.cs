using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Extensions;

public class EnumRangeAttribute : ValidationAttribute
{
    private int _minLength;
    private int _maxLength;
    public EnumRangeAttribute(Type obj)
    {
        _minLength = GetEnumMin(obj);
        _maxLength = GetEnumMax(obj);
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

    private int GetEnumMax<T>(T obj)
    {
        return Enum.GetValues(typeof(T)).Cast<int>().Max();
    }

    private int GetEnumMin<T>(T obj)
    {
        return Enum.GetValues(typeof(T)).Cast<int>().Min();
    }

}
