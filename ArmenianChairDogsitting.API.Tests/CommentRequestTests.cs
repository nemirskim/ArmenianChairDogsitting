using ArmenianChairDogsitting.API.Infrastructure;
using ArmenianChairDogsitting.API.Models;
using System.ComponentModel.DataAnnotations;

namespace ArmenianChairDogsitting.API.Tests;

public class CommentRequestTests
{
    private List<ValidationResult> _validationResult;

    [SetUp]
    public void Setup()
    {        
        _validationResult = new List<ValidationResult>();
    }

    [Test]
    public void CommentRequest_ValidFill_ThenKeepProcessing()
    {
        // given
        var comment = new CommentRequest
        {
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
    public void CommentRequest_InValidFill_ThenBrakeProcessing()
    {
        // given
        var comment = new CommentRequest
        {
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
