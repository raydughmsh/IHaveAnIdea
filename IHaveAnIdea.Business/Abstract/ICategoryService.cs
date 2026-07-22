using IHaveAnIdea.Business.DTOs;

namespace IHaveAnIdea.Business.Abstract;

public interface ICategoryService
{
    IList<CategoryDto> GetAll();
    CategoryDto? GetById(int id);
}
