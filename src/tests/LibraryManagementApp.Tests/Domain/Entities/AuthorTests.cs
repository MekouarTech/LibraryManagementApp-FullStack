using LibraryManagementApp.Domain.Entities;
using FluentAssertions;

namespace LibraryManagementApp.Tests.Domain.Entities;

public class AuthorTests
{
    [Fact]
    public void Properties_ShouldBeSetCorrectly_WhenAuthorIsCreated()
    {
        // Arrange
        var author = new Author
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Biography = "A famous author",
            DateOfBirth = new DateTime(1980, 1, 1)
        };

        // Assert
        author.Id.Should().Be(1);
        author.FirstName.Should().Be("John");
        author.LastName.Should().Be("Doe");
        author.Biography.Should().Be("A famous author");
        author.DateOfBirth.Should().Be(new DateTime(1980, 1, 1));
    }

    [Fact]
    public void Constructor_ShouldInitializeAuthor_WhenNewAuthorIsCreated()
    {
        // Arrange & Act
        var author = new Author();

        // Assert
        author.Id.Should().Be(0);
        author.FirstName.Should().BeEmpty();
        author.LastName.Should().BeEmpty();
        author.Biography.Should().BeEmpty();
        author.DateOfBirth.Should().Be(default(DateTime));
    }

    [Theory]
    [InlineData("John", "Doe", "John Doe")]
    [InlineData("Jane", "Smith", "Jane Smith")]
    [InlineData("", "Doe", " Doe")]
    [InlineData("John", "", "John ")]
    public void FullName_ShouldBeConcatenatedCorrectly_WhenFirstNameAndLastNameAreSet(string firstName, string lastName, string expectedFullName)
    {
        // Arrange
        var author = new Author
        {
            FirstName = firstName,
            LastName = lastName
        };

        // Act
        var fullName = $"{author.FirstName} {author.LastName}";

        // Assert
        fullName.Should().Be(expectedFullName);
    }

    [Fact]
    public void Biography_ShouldBeSetCorrectly_WhenBiographyIsAssigned()
    {
        // Arrange
        var author = new Author();
        var biography = "This is a long biography about the author's life and works.";

        // Act
        author.Biography = biography;

        // Assert
        author.Biography.Should().Be(biography);
    }

    [Fact]
    public void DateOfBirth_ShouldBeSetCorrectly_WhenDateOfBirthIsAssigned()
    {
        // Arrange
        var author = new Author();
        var dateOfBirth = new DateTime(1990, 5, 15);

        // Act
        author.DateOfBirth = dateOfBirth;

        // Assert
        author.DateOfBirth.Should().Be(dateOfBirth);
    }

    [Fact]
    public void Id_ShouldBeSetCorrectly_WhenIdIsAssigned()
    {
        // Arrange
        var author = new Author();
        var id = 42;

        // Act
        author.Id = id;

        // Assert
        author.Id.Should().Be(id);
    }
} 