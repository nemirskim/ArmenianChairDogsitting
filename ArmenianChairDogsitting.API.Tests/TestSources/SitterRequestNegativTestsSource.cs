using ArmenianChairDogsitting.API.Infrastructure;
using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.Data.Enums;
using System.Collections;

namespace ArmenianChairDogsitting.API.Tests.TestSources
{
    public class SitterRequestNegativTestsSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            SitterRequest sitter = GetSitterForTests();
            sitter.Name = "";
            yield return new object[]
            {
                sitter,
                ApiErrorMessage.NameIsRequired
            };

            sitter = GetSitterForTests();
            sitter.LastName = "";
            yield return new object[]
            {
                sitter,
                ApiErrorMessage.LastNameIsRequired
            };

            sitter = GetSitterForTests();
            sitter.Phone = "";
            yield return new object[]
            {
                sitter,
                ApiErrorMessage.PhoneIsRequired
            };

           /* sitter = GetSitterForTests();
            sitter.LastName = "888888";
            yield return new object[]
            {
                sitter,
                ApiErrorMessage.PhoneIsRange
            };*/

            sitter = GetSitterForTests();
            sitter.LastName = "8963";
            yield return new object[]
            {
                sitter,
                ApiErrorMessage.PhoneIsRange
            };

            sitter = GetSitterForTests();
            sitter.Email = "";
            yield return new object[]
            {
                sitter,
                ApiErrorMessage.EmailIsRequired
            };

            sitter = GetSitterForTests();
            sitter.Email = "pistolpis.com";
            yield return new object[]
            {
                sitter,
                ApiErrorMessage.EmailCharacterIsRequired
            };

            sitter = GetSitterForTests();
            sitter.Password = "";
            yield return new object[]
            {
                sitter,
                ApiErrorMessage.PasswordIsRequired
            };

            sitter = GetSitterForTests();
            sitter.Password = "83849";
            yield return new object[]
            {
                sitter,
                ApiErrorMessage.PasswordLenghtIsLess
            };

            sitter = GetSitterForTests();
            sitter.Age = 3849;
            yield return new object[]
            {
                sitter,
                ApiErrorMessage.AgeIsRange
            };

/*            sitter = GetSitterForTests();
            sitter.Experience = 0;
            yield return new object[]
            {
                sitter,
                ApiErrorMessage.ExperienceIsRequired
            };*/

/*            sitter = GetSitterForTests();
            sitter.Sex;
            yield return new object[]
            {
                sitter,
                ApiErrorMessage.AgeIsRequired
            };*/
        }

        private SitterRequest GetSitterForTests()
        {
            return new SitterRequest
            {
                Name = "Alex",
                LastName = "Pistoletov",
                Phone = "89991116116",
                Email = "pistol@pi.com",
                Password = "123456789",
                Age = 27,
                Experience = 7,
                Sex = Sex.Male
            };
        }
    }
}
