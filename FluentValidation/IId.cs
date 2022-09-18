namespace FluentValidation
{
    public interface IId
    {
        public int Id { get; set; }
    }

    public class IdValidator : AbstractValidator<IId>
    {
        public IdValidator()
        {
            var id = 1;
            RuleFor(x => x.Id).Equal(id).WithMessage($"{nameof(id)} must be {id}");
        }
    }
}