namespace Arena.Api.Tests;

public class ArenaControllerTests
{
    [Fact]
    public async Task Succesfull_Request_Should_Return_Ok_With_Guid()
    {
        var fighterServiceMock = new Mock<IFightingService>();

        const string expectedGuid = "1A3B944E-3632-467B-A53A-206305310BAE";

        fighterServiceMock.
            Setup(x => x.InitializeArena(It.IsAny<int>()))
            .ReturnsAsync(Guid.Parse(expectedGuid));

        var arenaController = new ArenaController(fighterServiceMock.Object);

        var response = await arenaController.GetIdentifier(10);

        response.As<OkObjectResult>().Value.Should().Be(Guid.Parse(expectedGuid));
    }

    [Fact]
    public async Task Failed_Request_Should_Return_BadRequest_With_Expected_Message()
    {
        var fighterServiceMock = new Mock<IFightingService>();

        fighterServiceMock.
            Setup(x => x.InitializeArena(It.IsAny<int>()))
            .Throws(new InitializingFailedException("Arena initialization failed."));

        var arenaController = new ArenaController(fighterServiceMock.Object);

        var response = await arenaController.GetIdentifier(10);

        response.As<BadRequestObjectResult>().Value.Should().Be("Arena initialization failed.");
    }

    [Fact]
    public async Task Invalid_Count_Request_Should_Return_BadRequest_With_Expected_Message()
    {
        var fighterServiceMock = new Mock<IFightingService>();

        const int count = 0;

        fighterServiceMock.
            Setup(x => x.InitializeArena(It.IsAny<int>()))
            .Throws(new ArgumentException("Arena count cannot be null.", nameof(count)));

        var arenaController = new ArenaController(fighterServiceMock.Object);

        var response = await arenaController.GetIdentifier(count);

        response.As<BadRequestObjectResult>().Value.Should().Be("Arena count cannot be null. (Parameter 'count')");
    }

    [Fact]
    public void Throw_Exception_When_Unexpected_Error_Occoured()
    {
        var fighterServiceMock = new Mock<IFightingService>();

        const int count = 0;

        fighterServiceMock.
            Setup(x => x.InitializeArena(It.IsAny<int>()))
            .Throws(new Exception());

        var arenaController = new ArenaController(fighterServiceMock.Object);

        var action = () => arenaController.GetIdentifier(count);

        action.Should().ThrowAsync<Exception>();
    }
}