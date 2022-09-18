using FluentValidation.Results;

namespace FluentValidation
{
    public abstract class CompositeValidator<TClass> : AbstractValidator<TClass>, ICompositeValidator where TClass : class
    {
        private List<IValidator> otherValidators = new List<IValidator>();
        private TClass Instance;

        public CompositeValidator(TClass instance)
        {
            Instance = instance;
        }

        protected void RegisterBaseValidator<TValidator>(IValidator<TValidator> validator)
        {
            // Ensure that we've registered a compatible validator. 
            if (validator.CanValidateInstancesOfType(typeof(TClass)))
            {
                otherValidators.Add(validator);
            }
            else
            {
                throw new NotSupportedException($"Type {typeof(TValidator).Name} is not a base-class or interface implemented by {typeof(TClass).Name}.");
            }

        }


        public ValidationResult Validate()
        {
            ValidationContext<TClass> context = new ValidationContext<TClass>(Instance);
            var mainErrors = base.Validate(context).Errors;
            var errorsFromOtherValidators = otherValidators.SelectMany(x => x.Validate(context).Errors);
            var combinedErrors = mainErrors.Concat(errorsFromOtherValidators);

            return new ValidationResult(combinedErrors);
        }

        public ValidationResult ValidateAndThrow()
        {
            ValidationContext<TClass> context = new ValidationContext<TClass>(Instance);
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