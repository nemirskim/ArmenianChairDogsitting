using ArmenianChairDogsitting.API.Models;
using ArmenianChairDogsitting.API.Tests.TestSources;
using ArmenianChairDogsitting.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Tests;

public class SitterRequestValidationTests
{
    [Test]
    public void ClientRegisterRequest_SendingCorrectData_GetAnEmptyStringError()
    {
        //given
        var client = new SitterRequest
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

        var validationsResults = new List<ValidationResult>();

        //when
        var isValid = Validator.TryValidateObject(client, new ValidationContext(client), validationsResults, true);

        //then
        Assert.True(isValid);
    }

    [TestCaseSource(typeof(SitterRequestNegativTestsSource))]
    public void SitterRequest_SendingIncorrectData_GetErrorMessage(SitterRequest client, string errorMessage)
    {
        //given
        var validationsResults = new List<ValidationResult>();

        //when
        var isValid = Validator.TryValidateObject(client, new ValidationContext(client), validationsResults, true);

        //then
        var actualMessage = validationsResults[0].ErrorMessage;
        Assert.AreEqual(errorMessage, actualMessage);
    }
}
