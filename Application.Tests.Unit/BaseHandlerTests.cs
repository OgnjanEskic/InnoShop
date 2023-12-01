using Application.Validators;
using Domain.Interfaces;
using Domain.Models.Requests;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;

namespace Application.Tests.Unit
{
    public class BaseHandlerTests<T>
    {
        public Mock<ILocationRepository> locationRepositoryMock = null!;
        public Mock<ILogger<T>> logger = null!;
        public IValidator<LocationRequest> locationRequestValidator;

        public BaseHandlerTests()
        {
            locationRepositoryMock = new Mock<ILocationRepository>();
            logger = new Mock<ILogger<T>>();
            locationRequestValidator = new AddLocationRequestValidator();
        }
    }
}
