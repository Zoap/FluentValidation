using FluentValidation.Results;

namespace FluentValidation
{
    public abstract class ObjectValidation<TValidator> : ICompositeValidator where TValidator : class, ICompositeValidator
    {
        private readonly TValidator _validator;

        public ObjectValidation()
        {
            _validator = (TValidator)Activator.CreateInstance(typeof(TValidator), this);
        }

        public ValidationResult Validate()
        {
            return _validator.Validate();
        }

        public ValidationResult ValidateAndThrow()
        {
            return _validator.ValidateAndThrow();
        }
    }
}