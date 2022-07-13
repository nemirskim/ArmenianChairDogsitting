using ArmenianChairDogsitting.API.Infrastructure;
using ArmenianChairDogsitting.API.Models;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Tests;

public class CommentResponseTests
{
    private List<ValidationResult> _validationResult;

    [SetUp]
    public void Setup()
    {
        _validationResult = new List<ValidationResult>();
    }

    [Test]
    public void CommentResponse_ValidFill_ThenKeepProcessing()
    {
        // given
        var comment = new CommentResponse
        {
            Id = 1,
            OrderId = 2,
            Rating = 3,
            Text = "MinimanimanuMOO"
        };

        //when
        var ctx = new ValidationContext(comment, null, null);
        Validator.TryValidateObject(comment, ctx, _validationResult, true);

        //then
        Assert.IsTrue(_validationResult.Count == 0);
    }

    [Test]
    public void CommentResponse_InValidFill_ThenBrakeProcessing()
    {
        // given
        var comment = new CommentResponse
        {
            OrderId = 2,
            Rating = 7,
            Text = "MinimanimanuMOO"
        };

        //when
        var ctx = new ValidationContext(comment, null, null);
        Validator.TryValidateObject(comment, ctx, _validationResult, true);

        //then
        Assert.IsTrue(_validationResult.Count > 0);
    }
}
