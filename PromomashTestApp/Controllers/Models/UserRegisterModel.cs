using FluentValidation;
using PromomashTestApp.Infrastructure.Repositories;

namespace PromomashTestApp.Controllers.Models;

public class UserRegisterModel
{
    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool AgreedToWorkForFood { get; set; }

    public Guid CountryId { get; set; }

    public Guid ProvinceId { get; set; }
}

public class UserRegisterModelValidator : AbstractValidator<UserRegisterModel>
{
    public UserRegisterModelValidator(ICountryRepository countryRepository, IProvinceRepository provinceRepository)
    {
        RuleFor(user => user.Login)
            .NotNull()
            .EmailAddress()
            .WithMessage("Login must be a valid email");

        RuleFor(user => user.Password)
            .NotNull()
            .Must(x => x.Any(char.IsDigit))
            .Must(x => x.Any(char.IsLetter))
            .WithMessage("Password must contain min 1 digit and min 1 letter");

        RuleFor(user => user.AgreedToWorkForFood)
            .Must(x => x)
            .WithMessage("User must agree to work for food");

        RuleFor(user => user.CountryId).MustAsync(async (id, ct) =>
        {
            bool exists = await countryRepository.ExistsAsync(id, ct);
            return exists;
        }).WithMessage("Country is a required field");
        
        RuleFor(user => user).MustAsync(async (user, ct) =>
        {
            bool exists = await provinceRepository.ExistsAsync(user.ProvinceId, user.CountryId, ct);
            return exists;
        }).WithMessage("Province is a required field");
    }
}
