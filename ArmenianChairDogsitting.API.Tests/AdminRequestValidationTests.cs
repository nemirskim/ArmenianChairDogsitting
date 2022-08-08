using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.API.Tests.TestSources;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Tests;

public class AdminRequestValidationTests
{
    [Test]
    public void AdminRegisterRequest_SendingCorrectData_GetAnEmptyStringError()
    {
        //given
        var admin = new AdminRequest
        {
            Email = "pistol@pi.com",
            Password = "123456789",
        };

        var validationsResults = new List<ValidationResult>();

        //when
        var isValid = Validator.TryValidateObject(admin, new ValidationContext(admin), validationsResults, true);

        //then
        Assert.True(isValid);
    }

    [TestCaseSource(typeof(AdminRequestNegativTestsSource))]
    public void SitterRequest_SendingIncorrectData_GetErrorMessage(AdminRequest admin, string errorMessage)
    {
        //given
        var validationsResults = new List<ValidationResult>();

        //when
        var isValid = Validator.TryValidateObject(admin, new ValidationContext(admin), validationsResults, true);

        //then
        var actualMessage = validationsResults[0].ErrorMessage;
        Assert.AreEqual(errorMessage, actualMessage);
    }
}
