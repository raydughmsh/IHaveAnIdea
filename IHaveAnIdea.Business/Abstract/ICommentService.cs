using IHaveAnIdea.Business.DTOs;

namespace IHaveAnIdea.Business.Abstract;

public interface ICommentService
{
    IList<CommentDto> GetByPost(int postId);
    void Add(CommentCreateDto dto, string userId);
    void Delete(int id);
}
