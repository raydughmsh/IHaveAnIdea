using IHaveAnIdea.Business.DTOs;

namespace IHaveAnIdea.Business.Abstract;

public interface IPostService
{
    IList<PostDto> GetAll(string? currentUserId = null);
    IList<PostDto> GetByCategory(int categoryId, string? currentUserId = null);
    IList<PostDto> GetByUser(string userId);
    IList<PostDto> Search(string keyword, string? currentUserId = null);
    PostDto? GetById(int id, string? currentUserId = null);
    void Add(PostCreateDto dto, string userId);
    void Update(PostEditDto dto);
    void Delete(int id);
}
