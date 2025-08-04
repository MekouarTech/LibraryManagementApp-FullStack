using MediatR;
using LibraryManagementApp.Application.Categories.Commands.CreateCategory;
using LibraryManagementApp.Application.DTOs;
using LibraryManagementApp.Domain.Entities;
using LibraryManagementApp.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace LibraryManagementApp.Tests.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly CreateCategoryCommandHandler _handler;

    public CreateCategoryCommandHandlerTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _handler = new CreateCategoryCommandHandler(_mockUnitOfWork.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateCategorySuccessfully_WhenValidCommandIsProvided()
    {
        // Arrange
        var command = new CreateCategoryCommand
        {
            Name = "Fiction"
        };

        var createdCategory = new Category
        {
            Id = 1,
            Name = command.Name
        };

        _mockUnitOfWork.Setup(x => x.Categories.AddAsync(It.IsAny<Category>())).ReturnsAsync(createdCategory);
        _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Name.Should().Be("Fiction");

        _mockUnitOfWork.Verify(x => x.Categories.AddAsync(It.IsAny<Category>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Theory]
    [InlineData("Non-Fiction")]
    [InlineData("Science Fiction")]
    [InlineData("Mystery")]
    [InlineData("Romance")]
    [InlineData("Biography")]
    public async Task Handle_ShouldCreateCategoryWithDifferentNames_WhenDifferentNamesAreProvided(string categoryName)
    {
        // Arrange
        var command = new CreateCategoryCommand
        {
            Name = categoryName
        };

        var createdCategory = new Category
        {
            Id = 1,
            Name = command.Name
        };

        _mockUnitOfWork.Setup(x => x.Categories.AddAsync(It.IsAny<Category>())).ReturnsAsync(createdCategory);
        _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(categoryName);

        _mockUnitOfWork.Verify(x => x.Categories.AddAsync(It.IsAny<Category>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCreateCategoryWithEmptyName_WhenEmptyNameIsProvided()
    {
        // Arrange
        var command = new CreateCategoryCommand
        {
            Name = ""
        };

        var createdCategory = new Category
        {
            Id = 1,
            Name = command.Name
        };

        _mockUnitOfWork.Setup(x => x.Categories.AddAsync(It.IsAny<Category>())).ReturnsAsync(createdCategory);
        _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().BeEmpty();

        _mockUnitOfWork.Verify(x => x.Categories.AddAsync(It.IsAny<Category>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCreateCategoryWithNullName_WhenNullNameIsProvided()
    {
        // Arrange
        var command = new CreateCategoryCommand
        {
            Name = null!
        };

        var createdCategory = new Category
        {
            Id = 1,
            Name = command.Name
        };

        _mockUnitOfWork.Setup(x => x.Categories.AddAsync(It.IsAny<Category>())).ReturnsAsync(createdCategory);
        _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().BeNull();

        _mockUnitOfWork.Verify(x => x.Categories.AddAsync(It.IsAny<Category>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCreateCategoryWithLongName_WhenLongNameIsProvided()
    {
        // Arrange
        var longName = "This is a very long category name that might be used in real scenarios";
        var command = new CreateCategoryCommand
        {
            Name = longName
        };

        var createdCategory = new Category
        {
            Id = 1,
            Name = command.Name
        };

        _mockUnitOfWork.Setup(x => x.Categories.AddAsync(It.IsAny<Category>())).ReturnsAsync(createdCategory);
        _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(longName);

        _mockUnitOfWork.Verify(x => x.Categories.AddAsync(It.IsAny<Category>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCreateCategoryWithSpecialCharacters_WhenSpecialCharactersAreInName()
    {
        // Arrange
        var specialName = "Science & Technology (Advanced)";
        var command = new CreateCategoryCommand
        {
            Name = specialName
        };

        var createdCategory = new Category
        {
            Id = 1,
            Name = command.Name
        };

        _mockUnitOfWork.Setup(x => x.Categories.AddAsync(It.IsAny<Category>())).ReturnsAsync(createdCategory);
        _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(specialName);

        _mockUnitOfWork.Verify(x => x.Categories.AddAsync(It.IsAny<Category>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCreateMultipleCategories_WhenMultipleCommandsAreHandled()
    {
        // Arrange
        var commands = new[]
        {
            new CreateCategoryCommand { Name = "Fiction" },
            new CreateCategoryCommand { Name = "Non-Fiction" },
            new CreateCategoryCommand { Name = "Science Fiction" }
        };

        var createdCategories = new[]
        {
            new Category { Id = 1, Name = "Fiction" },
            new Category { Id = 2, Name = "Non-Fiction" },
            new Category { Id = 3, Name = "Science Fiction" }
        };

        _mockUnitOfWork.Setup(x => x.Categories.AddAsync(It.IsAny<Category>()))
            .ReturnsAsync((Category category) => createdCategories.First(c => c.Name == category.Name));
        _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var results = new List<CategoryDto>();
        foreach (var command in commands)
        {
            var result = await _handler.Handle(command, CancellationToken.None);
            results.Add(result);
        }

        // Assert
        results.Should().HaveCount(3);
        results[0].Name.Should().Be("Fiction");
        results[1].Name.Should().Be("Non-Fiction");
        results[2].Name.Should().Be("Science Fiction");

        _mockUnitOfWork.Verify(x => x.Categories.AddAsync(It.IsAny<Category>()), Times.Exactly(3));
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Exactly(3));
    }
} 