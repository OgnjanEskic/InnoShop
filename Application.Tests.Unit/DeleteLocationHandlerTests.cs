using Application.Commands;
using Application.Handlers;
using FluentResults;
using Moq;

namespace Application.Tests.Unit
{
    [TestFixture]
    public class DeleteLocationHandlerTests : BaseHandlerTests<DeleteLocationHandler>
    {

        [Test]
        public async Task Handle_CallsValidateAndCreateLocation_ReturnsSuccessFluentResult()
        {
            //Arrange
            var requestId = 1;
            var responseMock = Result.Ok();
            locationRepositoryMock.Setup(repository => repository.DeleteLocationAsync(requestId)).ReturnsAsync(responseMock);
            var handler = new DeleteLocationHandler(locationRepositoryMock.Object, logger.Object);
            var command = new DeleteLocationCommand(requestId);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            locationRepositoryMock.Verify(repository => repository.DeleteLocationAsync(requestId), Times.Once());
            Assert.That(result.IsSuccess);
        }
    }
}