using AutoMapper;
using IHaveAnIdea.Business.DTOs;
using IHaveAnIdea.Entity.Concrete;

namespace IHaveAnIdea.Business.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Post mappings
        CreateMap<Post, PostDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.AppUser.UserName))
            .ForMember(dest => dest.LikeCount, opt => opt.MapFrom(src => src.Likes.Count))
            .ForMember(dest => dest.CommentCount, opt => opt.MapFrom(src => src.Comments.Count))
            .ForMember(dest => dest.IsLikedByCurrentUser, opt => opt.Ignore());

        CreateMap<PostCreateDto, Post>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true));

        CreateMap<PostEditDto, Post>()
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

        // Comment mappings
        CreateMap<Comment, CommentDto>()
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.AppUser.UserName));

        CreateMap<CommentCreateDto, Comment>()
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

        // Category mappings
        CreateMap<Category, CategoryDto>().ReverseMap();
    }
}
