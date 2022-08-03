using ArmenianChairDogsitting.API.Infrastructure;
using ArmenianChairDogsitting.API.Models;
using System.Collections;

namespace ArmenianChairDogsitting.API.Tests.TestSources;

public class AdminRequestNegativTestsSource : IEnumerable
{
    public IEnumerator GetEnumerator()
    {
        AdminRequest admin = GetAdminForTests();
        admin.Password = "";
        yield return new object[]
        {
            admin,
            ApiErrorMessage.PasswordIsRequired
        };

        admin = GetAdminForTests();
        admin.Email = "";
        yield return new object[]
        {
            admin,
            ApiErrorMessage.EmailIsRequired
        };

        admin = GetAdminForTests();
        admin.Password = "12233";
        yield return new object[]
        {
            admin,
            ApiErrorMessage.PasswordLenghtIsLess
        };

        admin = GetAdminForTests();
        admin.Email = "lamdlamwfm";
        yield return new object[]
        {
            admin,
            ApiErrorMessage.EmailCharacterIsRequired
        };
    }


    private AdminRequest GetAdminForTests()
    {
        return new AdminRequest
        {
            Email = "pistol@pi.com",
            Password = "123456789",
        };
    }
}
