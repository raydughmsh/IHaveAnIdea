using FluentValidation;
using IHaveAnIdea.Business.DTOs;

namespace IHaveAnIdea.Business.ValidationRules;

public class LoginValidator : AbstractValidator<UserLoginDto>
{
    public LoginValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-posta alanı boş bırakılamaz.")
            .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifre alanı boş bırakılamaz.");
    }
}
