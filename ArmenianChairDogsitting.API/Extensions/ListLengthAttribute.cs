using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Extensions;

public class ListLengthAttribute : ValidationAttribute
{
    private int _minimum;
    private int _maximum;

    public ListLengthAttribute(int min = int.MinValue, int max = int.MaxValue)
    {
        _minimum = min;
        _maximum = max;
    }

    public override bool IsValid(object value)
    {
        var list = value as IList;
        if (list is null
            || list.Count < _minimum
            || list.Count > _maximum)
        {
            return false;
        }
        return true;
    }
}
