using MediatR;
using LibraryManagementApp.Application.Publishers.Commands.CreatePublisher;
using LibraryManagementApp.Application.DTOs;
using LibraryManagementApp.Domain.Entities;
using LibraryManagementApp.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace LibraryManagementApp.Tests.Application.Publishers.Commands.CreatePublisher;

public class CreatePublisherCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly CreatePublisherCommandHandler _handler;

    public CreatePublisherCommandHandlerTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _handler = new CreatePublisherCommandHandler(_mockUnitOfWork.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreatePublisherSuccessfully_WhenValidCommandIsProvided()
    {
        // Arrange
        var command = new CreatePublisherCommand
        {
            Name = "Penguin Books"
        };

        var createdPublisher = new Publisher
        {
            Id = 1,
            Name = command.Name
        };

        _mockUnitOfWork.Setup(x => x.Publishers.AddAsync(It.IsAny<Publisher>())).ReturnsAsync(createdPublisher);
        _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Name.Should().Be("Penguin Books");

        _mockUnitOfWork.Verify(x => x.Publishers.AddAsync(It.IsAny<Publisher>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Theory]
    [InlineData("Random House")]
    [InlineData("HarperCollins")]
    [InlineData("Simon & Schuster")]
    [InlineData("Macmillan")]
    [InlineData("Hachette Book Group")]
    public async Task Handle_ShouldCreatePublisherWithDifferentNames_WhenDifferentNamesAreProvided(string publisherName)
    {
        // Arrange
        var command = new CreatePublisherCommand
        {
            Name = publisherName
        };

        var createdPublisher = new Publisher
        {
            Id = 1,
            Name = command.Name
        };

        _mockUnitOfWork.Setup(x => x.Publishers.AddAsync(It.IsAny<Publisher>())).ReturnsAsync(createdPublisher);
        _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(publisherName);

        _mockUnitOfWork.Verify(x => x.Publishers.AddAsync(It.IsAny<Publisher>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCreatePublisherWithEmptyName_WhenEmptyNameIsProvided()
    {
        // Arrange
        var command = new CreatePublisherCommand
        {
            Name = ""
        };

        var createdPublisher = new Publisher
        {
            Id = 1,
            Name = command.Name
        };

        _mockUnitOfWork.Setup(x => x.Publishers.AddAsync(It.IsAny<Publisher>())).ReturnsAsync(createdPublisher);
        _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().BeEmpty();

        _mockUnitOfWork.Verify(x => x.Publishers.AddAsync(It.IsAny<Publisher>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCreatePublisherWithNullName_WhenNullNameIsProvided()
    {
        // Arrange
        var command = new CreatePublisherCommand
        {
            Name = null!
        };

        var createdPublisher = new Publisher
        {
            Id = 1,
            Name = command.Name
        };

        _mockUnitOfWork.Setup(x => x.Publishers.AddAsync(It.IsAny<Publisher>())).ReturnsAsync(createdPublisher);
        _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().BeNull();

        _mockUnitOfWork.Verify(x => x.Publishers.AddAsync(It.IsAny<Publisher>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCreatePublisherWithLongName_WhenLongNameIsProvided()
    {
        // Arrange
        var longName = "This is a very long publisher name that might be used in real scenarios";
        var command = new CreatePublisherCommand
        {
            Name = longName
        };

        var createdPublisher = new Publisher
        {
            Id = 1,
            Name = command.Name
        };

        _mockUnitOfWork.Setup(x => x.Publishers.AddAsync(It.IsAny<Publisher>())).ReturnsAsync(createdPublisher);
        _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(longName);

        _mockUnitOfWork.Verify(x => x.Publishers.AddAsync(It.IsAny<Publisher>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCreatePublisherWithSpecialCharacters_WhenSpecialCharactersAreInName()
    {
        // Arrange
        var specialName = "McGraw-Hill Education & Publishing";
        var command = new CreatePublisherCommand
        {
            Name = specialName
        };

        var createdPublisher = new Publisher
        {
            Id = 1,
            Name = command.Name
        };

        _mockUnitOfWork.Setup(x => x.Publishers.AddAsync(It.IsAny<Publisher>())).ReturnsAsync(createdPublisher);
        _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(specialName);

        _mockUnitOfWork.Verify(x => x.Publishers.AddAsync(It.IsAny<Publisher>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCreatePublisherWithNumbers_WhenNumbersAreInName()
    {
        // Arrange
        var nameWithNumbers = "Publisher 123 & Co.";
        var command = new CreatePublisherCommand
        {
            Name = nameWithNumbers
        };

        var createdPublisher = new Publisher
        {
            Id = 1,
            Name = command.Name
        };

        _mockUnitOfWork.Setup(x => x.Publishers.AddAsync(It.IsAny<Publisher>())).ReturnsAsync(createdPublisher);
        _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(nameWithNumbers);

        _mockUnitOfWork.Verify(x => x.Publishers.AddAsync(It.IsAny<Publisher>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCreateMultiplePublishers_WhenMultipleCommandsAreHandled()
    {
        // Arrange
        var commands = new[]
        {
            new CreatePublisherCommand { Name = "Penguin Books" },
            new CreatePublisherCommand { Name = "Random House" },
            new CreatePublisherCommand { Name = "HarperCollins" }
        };

        var createdPublishers = new[]
        {
            new Publisher { Id = 1, Name = "Penguin Books" },
            new Publisher { Id = 2, Name = "Random House" },
            new Publisher { Id = 3, Name = "HarperCollins" }
        };

        _mockUnitOfWork.Setup(x => x.Publishers.AddAsync(It.IsAny<Publisher>()))
            .ReturnsAsync((Publisher publisher) => createdPublishers.First(p => p.Name == publisher.Name));
        _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var results = new List<PublisherDto>();
        foreach (var command in commands)
        {
            var result = await _handler.Handle(command, CancellationToken.None);
            results.Add(result);
        }

        // Assert
        results.Should().HaveCount(3);
        results[0].Name.Should().Be("Penguin Books");
        results[1].Name.Should().Be("Random House");
        results[2].Name.Should().Be("HarperCollins");

        _mockUnitOfWork.Verify(x => x.Publishers.AddAsync(It.IsAny<Publisher>()), Times.Exactly(3));
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Exactly(3));
    }

    [Fact]
    public async Task Handle_ShouldCreatePublisherWithUnicodeCharacters_WhenUnicodeCharactersAreInName()
    {
        // Arrange
        var unicodeName = "Éditions Gallimard & Cie";
        var command = new CreatePublisherCommand
        {
            Name = unicodeName
        };

        var createdPublisher = new Publisher
        {
            Id = 1,
            Name = command.Name
        };

        _mockUnitOfWork.Setup(x => x.Publishers.AddAsync(It.IsAny<Publisher>())).ReturnsAsync(createdPublisher);
        _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(unicodeName);

        _mockUnitOfWork.Verify(x => x.Publishers.AddAsync(It.IsAny<Publisher>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }
} 