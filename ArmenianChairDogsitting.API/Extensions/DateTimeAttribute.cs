using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Extensions
{
    public class DateTimeRequiredAttribute : ValidationAttribute
    {
        public DateTimeRequiredAttribute()
        {
            
        }

        public override bool IsValid(object value)
        {
            if ((DateTime)value == DateTime.MinValue || !(value is DateTime))
                return false;

            return true;
        }
    }
}
