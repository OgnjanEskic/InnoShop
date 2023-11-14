using Application.Commands;
using Application.Handlers;
using Application.Validators;
using Domain.Interfaces;
using Domain.Models.Requests;
using Domain.Models.Responses;
using FluentResults;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Moq;

namespace Application.Tests.Unit
{
    [TestFixture]
    public class CreateLocationHandlerTests
    {
        private IValidator<LocationRequest> _locationRequestValidator;
        private Mock<ILocationRepository> _locationRepositoryMock;
        private Mock<ILogger<CreateLocationHandler>> _logger;

        [SetUp]
        public void Setup()
        {
            _locationRequestValidator = new AddLocationRequestValidator();
            _locationRepositoryMock = new Mock<ILocationRepository>();
            _logger = new Mock<ILogger<CreateLocationHandler>>();
        }

        [Test]
        public async Task Handle_CallsValidateAndCreateLocation_ReturnsSuccessFluentResult()
        {
            //Arrange
            var request = new LocationRequest()
            {
                Description = "description",
                Name = "name",
            };
            var responseMock = new LocationResponse()
            {
                Id = 1,
                Name = "name",
                Description = "description"
            };
            _locationRepositoryMock.Setup(x => x.CreateLocation(request)).ReturnsAsync(responseMock);
            var handler = new CreateLocationHandler(_locationRequestValidator,
                _locationRepositoryMock.Object, _logger.Object);
            var command = new CreateLocationCommand(request);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            _locationRepositoryMock.Verify(x => x.CreateLocation(request), Times.Once());
            Assert.That(result.IsSuccess);
        }

        [Test]
        public async Task Handle_CallsValidateWithBadRequest_ReturnsValidationFailureError()
        {
            //Arrange
            var request = new LocationRequest()
            {
                Description = "description",
                Name = "",
            };
            var expectedValue = "Validation Failure";
            var handler = new CreateLocationHandler(_locationRequestValidator,
                _locationRepositoryMock.Object, _logger.Object);
            var command = new CreateLocationCommand(request);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(result.IsFailed);
                Assert.That(result.Errors.First().Reasons, Is.Not.Empty);
                Assert.That(expectedValue, Is.EqualTo(result.Errors.First().Message));
            });
        }

        [Test]
        public async Task Handle_CallsValidateAndCreateLocation_ReturnsFailedFluentResult()
        {
            //Arrange
            var request = new LocationRequest()
            {
                Description = "description",
                Name = "name",
            };
            var response = Result.Fail(new Error("Duplicated key"));

            _locationRepositoryMock.Setup(x => x.CreateLocation(request)).ReturnsAsync(response);
            var handler = new CreateLocationHandler(_locationRequestValidator,
                _locationRepositoryMock.Object, _logger.Object);
            var command = new CreateLocationCommand(request);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            _locationRepositoryMock.Verify(x => x.CreateLocation(request), Times.Once());
            Assert.That(result.IsFailed);
        }
    }
}