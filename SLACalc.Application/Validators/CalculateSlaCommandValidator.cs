using FluentValidation;
using SLACalc.Application.SLA.Commands;


namespace SlaCalculation.Application.Validators
{
    public class CalculateSlaCommandValidator : AbstractValidator<CalculateSlaCommand>
    {
        public CalculateSlaCommandValidator()
        {
            RuleFor(x => x.Priority)
                .NotEmpty().WithMessage("Priority is required")
                .MaximumLength(20).WithMessage("Priority must not exceed 20 characters");

            RuleFor(x => x.CaptureDateTime)
                .NotEmpty().WithMessage("Capture date time is required")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Capture date time cannot be in the future");

            RuleForEach(x => x.Files)
                .ChildRules(file =>
                {
                    file.RuleFor(f => f.FileName)
                        .NotEmpty().When(f => f != null)
                        .WithMessage("File name is required")
                        .MaximumLength(255).WithMessage("File name must not exceed 255 characters");
                })
                .When(x => x.Files != null);
        }
    }
}
