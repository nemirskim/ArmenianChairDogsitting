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
            Rating = 4,
            Text = "MinimanimanuMOO"
        };

        //when
        var ctx = new ValidationContext(comment, null, null);
        Validator.TryValidateObject(comment, ctx, _validationResult, true);

        //then
        Assert.IsTrue(_validationResult.Any(
            v =>
                v.MemberNames.Contains("Rating") &&
                v.MemberNames.Contains("Text") &&
                v.ErrorMessage!.Contains("Required") &&
                v.ErrorMessage.Contains("Range(0,5)")));
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
        Assert.IsTrue(_validationResult.Any(
            v =>
                v.MemberNames.Contains("Rating") &&
                v.MemberNames.Contains("Text") &&
                v.ErrorMessage!.Contains("Required") &&
                v.ErrorMessage.Contains("Range(0,5)")));
    }
}
