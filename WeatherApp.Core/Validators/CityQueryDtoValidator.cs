using FluentValidation;
using WeatherApp.Core.DTOs;
using WeatherApp.Core.Helpers;

namespace WeatherApp.Core.Validators;

public class CityQueryDtoValidator : AbstractValidator<CityQueryDto>
{
    public CityQueryDtoValidator()
    {
        RuleFor(x => x.CityName)
         .Custom((value, context) =>
         {
             if (string.IsNullOrWhiteSpace(value))
             {
                 context.AddFailure("A city name is required for the weather query.");
             }
             else if (!RegexHelper.IsPureEnglishRegex.IsMatch(value))
             {
                 context.AddFailure("The city name must contain only standard English");
             }
         });
    }
}
