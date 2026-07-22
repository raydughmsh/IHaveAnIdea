using IHaveAnIdea.Business.Abstract;
using IHaveAnIdea.DataAccess.Abstract;
using IHaveAnIdea.Entity.Concrete;

namespace IHaveAnIdea.Business.Concrete;

public class LikeManager : ILikeService
{
    private readonly ILikeRepository _likeRepository;

    public LikeManager(ILikeRepository likeRepository)
    {
        _likeRepository = likeRepository;
    }

    public bool Toggle(int postId, string userId)
    {
        var existing = _likeRepository.GetByPostAndUser(postId, userId);
        if (existing != null)
        {
            _likeRepository.Delete(existing);
            return false; // unliked
        }

        _likeRepository.Add(new Like
        {
            PostId = postId,
            AppUserId = userId,
            CreatedAt = DateTime.UtcNow
        });
        return true; // liked
    }

    public int GetCount(int postId) => _likeRepository.GetLikeCount(postId);

    public bool IsLiked(int postId, string userId) =>
        _likeRepository.GetByPostAndUser(postId, userId) != null;
}
