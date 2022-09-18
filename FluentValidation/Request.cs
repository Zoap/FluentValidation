namespace FluentValidation
{
    public class Request : IId, IValue, ICustomValidator<RequestValidator>
    {

        public Request()
        {
            InitValidator();
        }

        public Request(int id, string value)
        {
            Id = id;
            Value = value;
            InitValidator();
        }

        private void InitValidator()
        {
            Validator = new RequestValidator(this);
        }

        public int Id { get; set; }
        public string Value { get; set; }
        public RequestValidator Validator { get; private set; }
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