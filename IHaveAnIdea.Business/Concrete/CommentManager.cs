using AutoMapper;
using IHaveAnIdea.Business.Abstract;
using IHaveAnIdea.Business.DTOs;
using IHaveAnIdea.DataAccess.Abstract;
using IHaveAnIdea.Entity.Concrete;

namespace IHaveAnIdea.Business.Concrete;

public class CommentManager : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    public CommentManager(ICommentRepository commentRepository, IMapper mapper)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
    }

    public IList<CommentDto> GetByPost(int postId)
    {
        var comments = _commentRepository.GetByPostWithDetails(postId);
        return _mapper.Map<IList<CommentDto>>(comments);
    }

    public void Add(CommentCreateDto dto, string userId)
    {
        var comment = _mapper.Map<Comment>(dto);
        comment.AppUserId = userId;
        _commentRepository.Add(comment);
    }

    public void Delete(int id)
    {
        var comment = _commentRepository.GetById(id);
        if (comment == null) return;
        _commentRepository.Delete(comment);
    }
}
