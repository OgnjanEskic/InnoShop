using Application.Validators;
using Domain.Models.Requests;
using FluentValidation;

namespace Application.Tests.Unit
{
    [TestFixture]
    public class AddLocationRequestValidatorTests
    {
        IValidator<LocationRequest> _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new AddLocationRequestValidator();
        }

        [Test]
        public void Validate_CallingAddLocationRequestValidator_ResponseIsValid()
        {
            //Arrange
            var request = new LocationRequest()
            {
                Description = "description",
                Name = "name",
            };

            //Act
            var result = _validator.Validate(request);

            //Assert
            Assert.That(result.IsValid, Is.True);
        }

        [Test]
        public void Validate_CallingAddLocationRequestValidator_ResponseIsInvalid()
        {
            //Arrange
            var request = new LocationRequest()
            {
                Description = "description",
                Name = "",
            };

            //Act
            var result = _validator.Validate(request);

            //Assert
            Assert.That(result.Errors.Any(error => error.PropertyName.Contains("Name")), Is.True);
        }
    }
}
