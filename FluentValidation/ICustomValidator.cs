namespace FluentValidation
{
    public interface ICustomValidator<TValidator> where TValidator : class
    {
        public TValidator Validator { get; }
        private void InitValidator()
        {
        }
    }
}