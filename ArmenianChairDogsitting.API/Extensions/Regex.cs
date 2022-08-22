namespace ArmenianChairDogsitting.API.Extensions
{
    public static class Regex
    {
        public const string PhoneNumber = @"^((\+7|7|8)+([0-9]){10})$";
        public const string Email = @"^[^\s@]+@[^\s@]+\.[^\s@]+$";
    }
}
