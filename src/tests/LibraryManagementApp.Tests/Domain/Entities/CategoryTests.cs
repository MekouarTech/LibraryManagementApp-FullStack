using LibraryManagementApp.Domain.Entities;
using FluentAssertions;

namespace LibraryManagementApp.Tests.Domain.Entities;

public class CategoryTests
{
    [Fact]
    public void Properties_ShouldBeSetCorrectly_WhenCategoryIsCreated()
    {
        // Arrange
        var category = new Category
        {
            Id = 1,
            Name = "Fiction"
        };

        // Assert
        category.Id.Should().Be(1);
        category.Name.Should().Be("Fiction");
    }

    [Fact]
    public void Constructor_ShouldInitializeCategory_WhenNewCategoryIsCreated()
    {
        // Arrange & Act
        var category = new Category();

        // Assert
        category.Id.Should().Be(0);
        category.Name.Should().BeEmpty();
    }

    [Theory]
    [InlineData("Fiction")]
    [InlineData("Non-Fiction")]
    [InlineData("Science Fiction")]
    [InlineData("Mystery")]
    [InlineData("Romance")]
    public void Name_ShouldBeSetCorrectly_WhenCategoryNameIsAssigned(string categoryName)
    {
        // Arrange
        var category = new Category();

        // Act
        category.Name = categoryName;

        // Assert
        category.Name.Should().Be(categoryName);
    }

    [Fact]
    public void Id_ShouldBeSetCorrectly_WhenIdIsAssigned()
    {
        // Arrange
        var category = new Category();
        var id = 15;

        // Act
        category.Id = id;

        // Assert
        category.Id.Should().Be(id);
    }

    [Fact]
    public void EmptyName_ShouldBeSetCorrectly_WhenEmptyNameIsAssigned()
    {
        // Arrange
        var category = new Category();

        // Act
        category.Name = "";

        // Assert
        category.Name.Should().BeEmpty();
    }

    [Fact]
    public void NullName_ShouldBeSetCorrectly_WhenNullNameIsAssigned()
    {
        // Arrange
        var category = new Category();

        // Act
        category.Name = null!;

        // Assert
        category.Name.Should().BeNull();
    }
} 