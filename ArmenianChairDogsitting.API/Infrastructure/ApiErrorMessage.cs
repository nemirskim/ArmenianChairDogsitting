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
        public const string DogQuantityError = "Dog's quantity have to be less than 4 and more than 1";
        public const string RatingRange = "Rating is out of range";
        public const string SexRange = "The gender was entered incorrectly";
        public const string TextIsRequired = "Text is required";
        public const string IdIsRequired = "Id is required";
        public const string PriceCatalogIsRequired = "The service catalog is empty";
        public const string ServiceIsRequired = "Service was not entered";
        public const string PriceIsRequired = "Price was not entered";
    }
}
