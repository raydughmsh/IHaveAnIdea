using AutoMapper;
using IHaveAnIdea.Business.Abstract;
using IHaveAnIdea.Business.DTOs;
using IHaveAnIdea.DataAccess.Abstract;
using IHaveAnIdea.Entity.Concrete;

namespace IHaveAnIdea.Business.Concrete;

public class PostManager : IPostService
{
    private readonly IPostRepository _postRepository;
    private readonly ILikeRepository _likeRepository;
    private readonly IMapper _mapper;

    public PostManager(IPostRepository postRepository, ILikeRepository likeRepository, IMapper mapper)
    {
        _postRepository = postRepository;
        _likeRepository = likeRepository;
        _mapper = mapper;
    }

    public IList<PostDto> GetAll(string? currentUserId = null)
    {
        var posts = _postRepository.GetAllWithDetails();
        return MapWithLikeStatus(posts, currentUserId);
    }

    public IList<PostDto> GetByCategory(int categoryId, string? currentUserId = null)
    {
        var posts = _postRepository.GetByCategoryWithDetails(categoryId);
        return MapWithLikeStatus(posts, currentUserId);
    }

    public IList<PostDto> GetByUser(string userId)
    {
        var posts = _postRepository.GetByUserWithDetails(userId);
        return _mapper.Map<IList<PostDto>>(posts);
    }

    public IList<PostDto> Search(string keyword, string? currentUserId = null)
    {
        var posts = _postRepository.GetAllWithDetails()
            .Where(p => p.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                     || p.Content.Contains(keyword, StringComparison.OrdinalIgnoreCase))
            .ToList();
        return MapWithLikeStatus(posts, currentUserId);
    }

    public PostDto? GetById(int id, string? currentUserId = null)
    {
        var post = _postRepository.GetByIdWithDetails(id);
        if (post == null) return null;

        var dto = _mapper.Map<PostDto>(post);
        if (currentUserId != null)
            dto.IsLikedByCurrentUser = _likeRepository.GetByPostAndUser(id, currentUserId) != null;
        return dto;
    }

    public void Add(PostCreateDto dto, string userId)
    {
        var post = _mapper.Map<Post>(dto);
        post.AppUserId = userId;
        _postRepository.Add(post);
    }

    public void Update(PostEditDto dto)
    {
        var post = _postRepository.GetById(dto.Id);
        if (post == null) return;

        post.Title = dto.Title;
        post.Content = dto.Content;
        post.CategoryId = dto.CategoryId;
        post.UpdatedAt = DateTime.UtcNow;
        _postRepository.Update(post);
    }

    public void Delete(int id)
    {
        var post = _postRepository.GetById(id);
        if (post == null) return;
        _postRepository.Delete(post);
    }

    private IList<PostDto> MapWithLikeStatus(IList<Post> posts, string? currentUserId)
    {
        var dtos = _mapper.Map<IList<PostDto>>(posts);
        if (currentUserId != null)
        {
            foreach (var dto in dtos)
                dto.IsLikedByCurrentUser = _likeRepository.GetByPostAndUser(dto.Id, currentUserId) != null;
        }
        return dtos;
    }
}
