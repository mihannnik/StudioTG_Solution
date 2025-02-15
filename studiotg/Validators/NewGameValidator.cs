using FluentValidation;
using Microsoft.Extensions.Options;
using StudioTG.Application.DTO.Requests;
using StudioTG.Infrastructure.Common;

namespace StudioTG.Web.Validators
{
    public class NewGameValidator : AbstractValidator<NewGameRequest>
    {
        private readonly FieldOptions fieldOptions;
        public NewGameValidator(IOptions<FieldOptions> options)
        {
            fieldOptions = options.Value;
            RuleFor(r => r.Width)
                .InclusiveBetween(0, fieldOptions.MaxWidth)
                .WithErrorCode($"Width should be between 0 and {fieldOptions.MaxWidth}");
            RuleFor(r => r.Height)
                .InclusiveBetween(0, fieldOptions.MaxHeight)
                .WithMessage($"Width should be between 0 and {fieldOptions.MaxHeight}");
            RuleFor(r => r.MinesCount)
                .GreaterThan(0)
                .LessThan(r => r.Height * r.Width)
                .WithMessage(r => $"Mines should be more then 0 and less then {r.Width * r.Height}");
        }
    }
}
