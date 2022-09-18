namespace FluentValidation
{
    public interface IRequest : IId, IValue, ICompositeValidator
    {
    }

    public class RequestValidator : CompositeValidator<Request>
    {
        public RequestValidator(Request instance) : base(instance)
        {
            RegisterBaseValidator(new IdValidator());
            RegisterBaseValidator(new ValueValidator());
        }
    }
}