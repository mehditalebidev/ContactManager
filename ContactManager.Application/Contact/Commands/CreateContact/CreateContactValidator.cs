using System.Text.RegularExpressions;
using FluentValidation;

namespace ContactManager.Application.Contact.Commands.CreateContact;

public class CreateContactValidator : AbstractValidator<CreateContactCommand>
{
    public CreateContactValidator()
    {
        RuleFor(x => x.Salutation).MinimumLength(2).MaximumLength(50).NotEmpty();
        RuleFor(x => x.Firstname).MinimumLength(2).MaximumLength(50).NotEmpty();
        RuleFor(x => x.Lastname).MinimumLength(2).MaximumLength(50).NotEmpty();
        RuleFor(x => x.DisplayName).MinimumLength(2).MaximumLength(150);
        RuleFor(x => x.Email).NotEmpty().MaximumLength(255).EmailAddress();
        RuleFor(x => x.PhoneNumber)
            .Matches(new Regex(@"^[\+]?[(]?[0-9]{3}[)]?[-\s\.]?[0-9]{3}[-\s\.]?[0-9]{4,6}$")).WithMessage("PhoneNumber not valid");
        RuleFor(x => x.Birthdate).LessThan(DateTime.Now).WithMessage("Birthdate must be in the past");
    }
}