using FluentValidation;

namespace Validation.Shared
{
    public class TodoItemValidator : AbstractValidator<ITodoItem>
    {
        //NOTE: Even though EF gives attributes for DataObjects for server validations - great to have validators that can be injected based one scenarios, and shared with the xamarin client
        //NOTE: Validated against an interface, so both server data objects and client bindable models can be validated using the same validators

        public TodoItemValidator()
        {
            //TODO: this error needs to be a resource for language translations
            RuleFor(x => x.Text).NotEmpty().WithMessage("TodoItem Text cannot be empty");
            RuleFor(x => x.Email).NotEmpty().WithMessage("TodoItem Text cannot be empty").EmailAddress().WithMessage("Email must be in a valid format");
        }
    }
}