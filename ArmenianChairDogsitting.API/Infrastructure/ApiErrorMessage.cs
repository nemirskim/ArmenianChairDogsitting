namespace ArmenianChairDogsitting.API.Infrastructure
{
    public class ApiErrorMessage
    {
        public const string NameIsRequired = "Name was not entered";
        public const string LastNameIsRequired = "LastName was not entered";
        public const string PasswordIsRequired = "Password was not entered";
        public const string PasswordLenghtIsLess = "Password length is less than 8 characters";
        public const string EmailIsRequired = "Email was not entered";
        public const string EmailCharacterIsRequired = "Email number is incorrect";
        public const string PhoneIsRequired = "Phone was not entered";
        public const string PhoneIsRange = "Phone number is incorrect";
        public const string AgeIsRequired = "Age was not entered";
        public const string AgeIsRange = "Age out of range";
        public const string BreedIsRequired = "Breed was not entered";
        public const string SizeIsRequired = "Size was not entered";
        public const string ExperienceIsRequired = "Experience was not entered";
        public const string SexIsRequired = "Sex was not entered";
    }
}
