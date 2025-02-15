using FluentValidation;
using Microsoft.Extensions.Options;
using StudioTG.Application.DTO.Requests;
using StudioTG.Infrastructure.Common;

namespace StudioTG.Web.Validators
{
    public class MakeTurnValidator : AbstractValidator<MakeTurnRequest>
    {
        private readonly FieldOptions fieldOptions;
        public MakeTurnValidator(IOptions<FieldOptions> options)
        {
            fieldOptions = options.Value;
            RuleFor(r => r.Id)
                .NotNull()
                .NotEmpty()
                .WithMessage("Game id should be not null or empty");
            RuleFor(r => r.Row)
                .GreaterThanOrEqualTo(0)
                .LessThan(fieldOptions.MaxWidth)
                .WithMessage($"Width should be between 0 and {fieldOptions.MaxWidth}");
            RuleFor(r => r.Collumn)
                .GreaterThanOrEqualTo(0)
                .LessThan(fieldOptions.MaxHeight)
                .WithMessage($"Width should be between 0 and {fieldOptions.MaxHeight}");
        }
    }
}
