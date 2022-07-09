using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Extensions;

public class ListLengthAttribute : ValidationAttribute
{
    public int Minimum { get; set; }
    public int Maximum { get; set; }

    public ListLengthAttribute(int min = int.MinValue, int max = int.MaxValue)
    {
        Minimum = min;
        Maximum = max;
    }

    public override bool IsValid(object value)
    {
        var list = value as IList;
        if (list is null
            || list.Count < Minimum 
            || list.Count > Maximum)
        {
            return false;
        }
        return true;
    }
}
