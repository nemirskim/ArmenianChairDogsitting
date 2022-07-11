using ArmenianChairDogsitting.Business.Interfaces;
using ArmenianChairDogsitting.Business.Models;
using Moq;

namespace ArmenianChairDogsitting.API.Tests;

public class MockCommentsService : Mock<ICommentsService>
{
    public MockCommentsService MockAddComment(CommentModel commentToAdd)
    {
        Setup(x => x.AddComment(commentToAdd))
            .Returns(commentToAdd.Id);

        return this;
    }

}
