using FluentValidation;
using IHaveAnIdea.Business.DTOs;

namespace IHaveAnIdea.Business.ValidationRules;

public class PostValidator : AbstractValidator<PostCreateDto>
{
    public PostValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Başlık alanı boş bırakılamaz.")
            .MinimumLength(5).WithMessage("Başlık en az 5 karakter olmalıdır.")
            .MaximumLength(100).WithMessage("Başlık en fazla 100 karakter olabilir.");

        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("İçerik alanı boş bırakılamaz.")
            .MinimumLength(10).WithMessage("İçerik en az 10 karakter olmalıdır.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("Lütfen bir kategori seçiniz.");
    }
}
