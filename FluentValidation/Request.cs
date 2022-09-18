﻿using FluentValidation.Results;

namespace FluentValidation
{
    public class Request : IRequest
    {

        public int Id { get; set; }
        public string Value { get; set; }
        public RequestValidator Validator { get; private set; }

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

        public void InitValidator()
        {
            Validator = new RequestValidator(this);
        }

        public ValidationResult Validate()
        {
            return Validator.Validate();
        }

        public ValidationResult ValidateAndThrow()
        {
            return Validator.ValidateAndThrow();
        }
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