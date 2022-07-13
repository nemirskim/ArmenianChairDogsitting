//using ArmenianChairDogsitting.Data.Entities;
//using ArmenianChairDogsitting.Data.Repositories;
//using Moq;

//namespace ArmenianChairDogSitting.Business.Tests;

//public class MockCommentsRepository : Mock<ICommentsRepository>
//{
//    public MockCommentsRepository MockGetAllComments(List<Comment> result)
//    {
//        Setup(x => x.GetAllComments())
//            .Returns(result);

//        return this;
//    }

//    public MockCommentsRepository MockAddComment(Comment commentToAdd)
//    {
//        Setup(x => x.AddComment(commentToAdd))
//            .Returns(commentToAdd.Id);

//        return this;
//    }

//    public MockCommentsRepository MockDeleteById(int id)
//    {
//        Setup(x => x.DeleteCommentById(id));

//        return this;
//    }

//    public MockCommentsRepository MockGetById(int id, Comment returns)
//    {        
//        Setup(x => x.GetCommentById(id))
//            .Returns(returns);

//        return this;
//    }
//}
