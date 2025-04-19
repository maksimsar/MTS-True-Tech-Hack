using FluentValidation;
using MTSTrueTechHack.Backend.Models;

namespace MTSTrueTechHack.Backend;

public sealed class CreateSchemaRequestValidator
    : AbstractValidator<CreateSchemaRequest>
{
    public CreateSchemaRequestValidator()
    {
        RuleFor(sch => sch.Name)
            .NotEmpty().WithMessage("Имя схемы обязательно")
            .MaximumLength(100);

        RuleFor(sch => sch.Description)
            .NotEmpty().WithMessage("Описание обязательно")
            .MaximumLength(500);
    }
}

public sealed class ChatRequestValidator
    : AbstractValidator<ChatRequest>
{
    public ChatRequestValidator()
    {
        RuleFor(chat => chat.Text)
            .NotEmpty()
            .MaximumLength(1_000);
    }
}