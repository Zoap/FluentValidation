using FluentValidation.Results;

namespace FluentValidation
{
    public abstract class CompositeValidator<TClass> : AbstractValidator<TClass>, ICompositeValidator where TClass : class
    {
        private readonly List<IValidator> _otherValidators;
        private readonly ValidationContext<TClass> _context;

        public CompositeValidator(TClass instance)
        {
            _context = new(instance);
            _otherValidators = new();
        }

        protected void RegisterBaseValidator<TValidator>(IValidator<TValidator> validator)
        {
            // Ensure that we've registered a compatible validator. 
            if (validator.CanValidateInstancesOfType(typeof(TClass)))
            {
                _otherValidators.Add(validator);
            }
            else
            {
                throw new NotSupportedException($"Type {typeof(TValidator).Name} is not a base-class or interface implemented by {typeof(TClass).Name}.");
            }

        }

        public ValidationResult Validate()
        {
            var mainErrors = base.Validate(_context).Errors;
            var errorsFromOtherValidators = _otherValidators.SelectMany(x => x.Validate(_context).Errors);
            var combinedErrors = mainErrors.Concat(errorsFromOtherValidators);

            return new ValidationResult(combinedErrors);
        }

        public ValidationResult ValidateAndThrow()
        {
            var result = Validate();

            if (result.Errors.Count() > 0)
            {
                throw new Exception(string.Join('\n', result.Errors.Select(x => x.ErrorMessage).Distinct().ToList()));
            }

            return result;
        }
    }
}