using FluentValidation.Results;

namespace FluentValidation
{
    public interface ICustomValidator<TValidator> where TValidator : class, ICompositeValidator
    {
        TValidator Validator { get; }
        void InitValidator();
        ValidationResult Validate();
        ValidationResult ValidateAndThrow();
    }
}