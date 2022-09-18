namespace FluentValidation
{
    public interface IValue
    {
        public string Value { get; set; }
    }

    public class ValueValidator : AbstractValidator<IValue>
    {
        public ValueValidator()
        {
            RuleFor(x => x.Value)
                .NotNull()
                .NotEmpty()
                .MinimumLength(6);
        }
    }
}