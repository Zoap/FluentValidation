using FluentAssertions;
using NUnit.Framework;
using System;
using System.Diagnostics;

namespace FluentValidation.Test
{
    public class ValidatorTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void RequestValidatorTest()
        {
            IRequest request = new Request(1, "HelloWorld"); //valid
            var result = request.Validate();
            result.IsValid.Should().BeTrue();

            request = new Request(1, ""); //invalid
            result = request.Validate();
            result.IsValid.Should().BeFalse();

            request = new Request(2, "HelloWorld"); //invalid
            result = request.Validate();
            result.IsValid.Should().BeFalse();

            var exceptionThrown = false;
            try
            {
                request.ValidateAndThrow();
            }
            catch (Exception)
            {
                exceptionThrown = true;
            }

            exceptionThrown.Should().BeTrue();


            Debug.WriteLine(result.IsValid);
        }
    }
}