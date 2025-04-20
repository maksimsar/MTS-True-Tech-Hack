using System.Linq;
using FluentValidation;
using MTSTrueTechHack.Backend.Models;

namespace MTSTrueTechHack.Backend.Validators
{
    public sealed class CreateSchemaRequestValidator
        : AbstractValidator<CreateSchemaRequest>
    {
        public CreateSchemaRequestValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .WithMessage("UserId must be greater than 0");

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MaximumLength(100)
                .WithMessage("Name must be at most 100 characters");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required")
                .MaximumLength(500)
                .WithMessage("Description must be at most 500 characters");
        }
    }

    public sealed class ChatRequestValidator
        : AbstractValidator<ChatRequest>
    {
        public ChatRequestValidator()
        {
            RuleFor(x => x.Message)
                .NotEmpty()
                .WithMessage("Message is required")
                .MaximumLength(1000)
                .WithMessage("Message must be at most 1000 characters");

            RuleFor(x => x.History)
                .NotNull()
                .WithMessage("History cannot be null")
                .Must(h => h.Count() <= 1000)
                .WithMessage("History can contain at most 1000 messages");
        }
    }
}