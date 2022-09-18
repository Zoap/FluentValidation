using FluentValidation.Results;

namespace FluentValidation
{
    public interface ICompositeValidator
    {
        ValidationResult Validate();
        ValidationResult ValidateAndThrow();
    }
}