namespace FluentValidation
{
    public class Request : ObjectValidation<RequestValidator>, IRequest
    {

        public int Id { get; set; }
        public string Value { get; set; }

        public Request(int id, string value)
        {
            Id = id;
            Value = value;
        }
    }
}