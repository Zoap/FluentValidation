namespace FluentValidation
{
    public interface IRequest : IId, IValue, ICustomValidator<RequestValidator>
    {
    }
}