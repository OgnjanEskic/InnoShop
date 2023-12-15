using Application.Commands;
using Application.Handlers;
using Domain.Models.Requests;
using Domain.Models.Responses;
using FluentResults;
using Moq;

namespace Application.Tests.Unit
{
    [TestFixture]
    public sealed class UpdateLocationHandlerTests : BaseHandlerTests<UpdateLocationHandler>
    {
        private readonly int id = 1;

        [Test]
        public async Task Handle_CallsValidateAndUpdateLocation_ReturnsSuccessFluentResult()
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
            locationRepositoryMock.Setup(repository => repository.UpdateLocationAsync(id, request)).ReturnsAsync(responseMock);
            var handler = new UpdateLocationHandler(locationRequestValidator, locationRepositoryMock.Object, logger.Object);
            var command = new UpdateLocationCommand(id, request);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            locationRepositoryMock.Verify(repository => repository.UpdateLocationAsync(id, request), Times.Once());
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
            var handler = new UpdateLocationHandler(locationRequestValidator, locationRepositoryMock.Object, logger.Object);
            var command = new UpdateLocationCommand(id, request);

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

            locationRepositoryMock.Setup(repository => repository.UpdateLocationAsync(id, request)).ReturnsAsync(response);
            var handler = new UpdateLocationHandler(locationRequestValidator,
                locationRepositoryMock.Object, logger.Object);
            var command = new UpdateLocationCommand(id, request);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            locationRepositoryMock.Verify(repository => repository.UpdateLocationAsync(id, request), Times.Once());
            Assert.That(result.IsFailed);
        }
    }
}