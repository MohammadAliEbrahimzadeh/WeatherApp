using FluentValidation;
using WeatherApp.Core.DTOs;
using WeatherApp.Core.Helpers;

namespace WeatherApp.Core.Validators;

public class CityQueryDtoValidator : AbstractValidator<CityQueryDto>
{
    public CityQueryDtoValidator()
    {
        RuleFor(x => x.CityName)
            .NotNull().NotEmpty()
            .WithMessage("A city name is required for the weather query.");

        RuleFor(x => x.CityName)
            .Must(text => RegexHelper.IsPureEnglishRegex.IsMatch(text!))
            .WithMessage("The city name must contain only standard English");
    }
}
