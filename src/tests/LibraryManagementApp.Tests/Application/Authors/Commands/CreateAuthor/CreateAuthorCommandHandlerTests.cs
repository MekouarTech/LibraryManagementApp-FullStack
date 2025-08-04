using MediatR;
using LibraryManagementApp.Application.Authors.Commands.CreateAuthor;
using LibraryManagementApp.Application.DTOs;
using LibraryManagementApp.Domain.Entities;
using LibraryManagementApp.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace LibraryManagementApp.Tests.Application.Authors.Commands.CreateAuthor;

public class CreateAuthorCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly CreateAuthorCommandHandler _handler;

    public CreateAuthorCommandHandlerTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _handler = new CreateAuthorCommandHandler(_mockUnitOfWork.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateAuthorSuccessfully_WhenValidCommandIsProvided()
    {
        // Arrange
        var command = new CreateAuthorCommand
        {
            FirstName = "John",
            LastName = "Doe",
            Biography = "A famous author",
            DateOfBirth = new DateTime(1980, 1, 1)
        };

        var createdAuthor = new Author
        {
            Id = 1,
            FirstName = command.FirstName,
            LastName = command.LastName,
            Biography = command.Biography,
            DateOfBirth = command.DateOfBirth
        };

        _mockUnitOfWork.Setup(x => x.Authors.AddAsync(It.IsAny<Author>())).ReturnsAsync(createdAuthor);
        _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.FirstName.Should().Be("John");
        result.LastName.Should().Be("Doe");
        result.Biography.Should().Be("A famous author");
        result.DateOfBirth.Should().Be(new DateTime(1980, 1, 1));

        _mockUnitOfWork.Verify(x => x.Authors.AddAsync(It.IsAny<Author>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCreateAuthorWithEmptyBiography_WhenBiographyIsEmpty()
    {
        // Arrange
        var command = new CreateAuthorCommand
        {
            FirstName = "Jane",
            LastName = "Smith",
            Biography = "",
            DateOfBirth = new DateTime(1990, 5, 15)
        };

        var createdAuthor = new Author
        {
            Id = 1,
            FirstName = command.FirstName,
            LastName = command.LastName,
            Biography = command.Biography,
            DateOfBirth = command.DateOfBirth
        };

        _mockUnitOfWork.Setup(x => x.Authors.AddAsync(It.IsAny<Author>())).ReturnsAsync(createdAuthor);
        _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Biography.Should().BeEmpty();

        _mockUnitOfWork.Verify(x => x.Authors.AddAsync(It.IsAny<Author>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCreateAuthorWithLongBiography_WhenBiographyIsLong()
    {
        // Arrange
        var longBiography = "This is a very long biography about the author's life and works. " +
                           "It contains many details about their childhood, education, career, " +
                           "and achievements in the literary world.";

        var command = new CreateAuthorCommand
        {
            FirstName = "Robert",
            LastName = "Johnson",
            Biography = longBiography,
            DateOfBirth = new DateTime(1975, 12, 25)
        };

        var createdAuthor = new Author
        {
            Id = 1,
            FirstName = command.FirstName,
            LastName = command.LastName,
            Biography = command.Biography,
            DateOfBirth = command.DateOfBirth
        };

        _mockUnitOfWork.Setup(x => x.Authors.AddAsync(It.IsAny<Author>())).ReturnsAsync(createdAuthor);
        _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Biography.Should().Be(longBiography);

        _mockUnitOfWork.Verify(x => x.Authors.AddAsync(It.IsAny<Author>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCreateAuthorWithFutureDateOfBirth_WhenDateOfBirthIsInFuture()
    {
        // Arrange
        var futureDate = DateTime.Now.AddYears(1);
        var command = new CreateAuthorCommand
        {
            FirstName = "Future",
            LastName = "Author",
            Biography = "An author from the future",
            DateOfBirth = futureDate
        };

        var createdAuthor = new Author
        {
            Id = 1,
            FirstName = command.FirstName,
            LastName = command.LastName,
            Biography = command.Biography,
            DateOfBirth = command.DateOfBirth
        };

        _mockUnitOfWork.Setup(x => x.Authors.AddAsync(It.IsAny<Author>())).ReturnsAsync(createdAuthor);
        _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.DateOfBirth.Should().Be(futureDate);

        _mockUnitOfWork.Verify(x => x.Authors.AddAsync(It.IsAny<Author>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCreateAuthorWithMinDateOfBirth_WhenDateOfBirthIsMinValue()
    {
        // Arrange
        var command = new CreateAuthorCommand
        {
            FirstName = "Ancient",
            LastName = "Author",
            Biography = "An ancient author",
            DateOfBirth = DateTime.MinValue
        };

        var createdAuthor = new Author
        {
            Id = 1,
            FirstName = command.FirstName,
            LastName = command.LastName,
            Biography = command.Biography,
            DateOfBirth = command.DateOfBirth
        };

        _mockUnitOfWork.Setup(x => x.Authors.AddAsync(It.IsAny<Author>())).ReturnsAsync(createdAuthor);
        _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.DateOfBirth.Should().Be(DateTime.MinValue);

        _mockUnitOfWork.Verify(x => x.Authors.AddAsync(It.IsAny<Author>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Theory]
    [InlineData("", "Doe")]
    [InlineData("John", "")]
    [InlineData("", "")]
    public async Task Handle_ShouldCreateAuthorWithEmptyNames_WhenNamesAreEmpty(string firstName, string lastName)
    {
        // Arrange
        var command = new CreateAuthorCommand
        {
            FirstName = firstName,
            LastName = lastName,
            Biography = "Test biography",
            DateOfBirth = new DateTime(1980, 1, 1)
        };

        var createdAuthor = new Author
        {
            Id = 1,
            FirstName = command.FirstName,
            LastName = command.LastName,
            Biography = command.Biography,
            DateOfBirth = command.DateOfBirth
        };

        _mockUnitOfWork.Setup(x => x.Authors.AddAsync(It.IsAny<Author>())).ReturnsAsync(createdAuthor);
        _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.FirstName.Should().Be(firstName);
        result.LastName.Should().Be(lastName);

        _mockUnitOfWork.Verify(x => x.Authors.AddAsync(It.IsAny<Author>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }
} 