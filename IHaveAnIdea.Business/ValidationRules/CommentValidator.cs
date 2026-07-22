using FluentValidation;
using IHaveAnIdea.Business.DTOs;

namespace IHaveAnIdea.Business.ValidationRules;

public class CommentValidator : AbstractValidator<CommentCreateDto>
{
    public CommentValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Yorum alanı boş bırakılamaz.")
            .MinimumLength(2).WithMessage("Yorum en az 2 karakter olmalıdır.")
            .MaximumLength(500).WithMessage("Yorum en fazla 500 karakter olabilir.");

        RuleFor(x => x.PostId)
            .GreaterThan(0).WithMessage("Geçersiz gönderi.");
    }
}
