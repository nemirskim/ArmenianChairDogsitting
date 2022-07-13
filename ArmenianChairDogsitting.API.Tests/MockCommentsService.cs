using ArmenianChairDogsitting.Business.Interfaces;
using ArmenianChairDogsitting.Data.Entities;
using Moq;

namespace ArmenianChairDogsitting.API.Tests;

public class MockCommentsService : Mock<ICommentsService>
{
    public MockCommentsService MockAddComment(Comment commentToAdd)
    {
        Setup(x => x.AddComment(It.IsAny<Comment>()))
            .Returns(commentToAdd.Id);

        return this;
    }

}
