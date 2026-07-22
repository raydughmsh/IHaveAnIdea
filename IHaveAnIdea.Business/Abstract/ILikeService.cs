namespace IHaveAnIdea.Business.Abstract;

public interface ILikeService
{
    bool Toggle(int postId, string userId);  // returns true if liked, false if unliked
    int GetCount(int postId);
    bool IsLiked(int postId, string userId);
}
