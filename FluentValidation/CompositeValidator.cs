using FluentValidation.Results;

namespace FluentValidation
{
    public abstract class CompositeValidator<T> : AbstractValidator<T>, ICompositeValidator
    {
        private List<IValidator> otherValidators = new List<IValidator>();
        private T Instance;

        public CompositeValidator(T instance)
        {
            Instance = instance;
        }

        protected void RegisterBaseValidator<TBase>(IValidator<TBase> validator)
        {
            // Ensure that we've registered a compatible validator. 
            if (validator.CanValidateInstancesOfType(typeof(T)))
            {
                otherValidators.Add(validator);
            }
            else
            {
                throw new NotSupportedException(string.Format("Type {0} is not a base-class or interface implemented by {1}.", typeof(TBase).Name, typeof(T).Name));
            }

        }

        public override ValidationResult Validate(ValidationContext<T> context)
        {
            var mainErrors = base.Validate(context).Errors;
            var errorsFromOtherValidators = otherValidators.SelectMany(x => x.Validate(context).Errors);
            var combinedErrors = mainErrors.Concat(errorsFromOtherValidators);

            return new ValidationResult(combinedErrors);
        }

        public ValidationResult Validate()
        {
            ValidationContext<T> context = new ValidationContext<T>(Instance);
            var mainErrors = base.Validate(context).Errors;
            var errorsFromOtherValidators = otherValidators.SelectMany(x => x.Validate(context).Errors);
            var combinedErrors = mainErrors.Concat(errorsFromOtherValidators);

            return new ValidationResult(combinedErrors);
        }

        public ValidationResult ValidateAndThrow()
        {
            ValidationContext<T> context = new ValidationContext<T>(Instance);
            var mainErrors = base.Validate(context).Errors;
            var errorsFromOtherValidators = otherValidators.SelectMany(x => x.Validate(context).Errors);
            var combinedErrors = mainErrors.Concat(errorsFromOtherValidators);

            if (combinedErrors.Count() > 0)
            {
                throw new Exception(string.Join('\n', combinedErrors.Select(x => x.ErrorMessage).Distinct().ToList()));
            }

            return new ValidationResult(combinedErrors);
        }
    }
}