using AutoMapper;
using IHaveAnIdea.Business.Abstract;
using IHaveAnIdea.Business.DTOs;
using IHaveAnIdea.DataAccess.Abstract;

namespace IHaveAnIdea.Business.Concrete;

public class CategoryManager : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    public CategoryManager(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    public IList<CategoryDto> GetAll()
    {
        var categories = _categoryRepository.GetAll();
        return _mapper.Map<IList<CategoryDto>>(categories);
    }

    public CategoryDto? GetById(int id)
    {
        var category = _categoryRepository.GetById(id);
        return category == null ? null : _mapper.Map<CategoryDto>(category);
    }
}
