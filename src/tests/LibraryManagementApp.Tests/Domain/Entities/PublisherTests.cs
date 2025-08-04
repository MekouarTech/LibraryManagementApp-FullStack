using LibraryManagementApp.Domain.Entities;
using FluentAssertions;

namespace LibraryManagementApp.Tests.Domain.Entities;

public class PublisherTests
{
    [Fact]
    public void Properties_ShouldBeSetCorrectly_WhenPublisherIsCreated()
    {
        // Arrange
        var publisher = new Publisher
        {
            Id = 1,
            Name = "Penguin Books"
        };

        // Assert
        publisher.Id.Should().Be(1);
        publisher.Name.Should().Be("Penguin Books");
    }

    [Fact]
    public void Constructor_ShouldInitializePublisher_WhenNewPublisherIsCreated()
    {
        // Arrange & Act
        var publisher = new Publisher();

        // Assert
        publisher.Id.Should().Be(0);
        publisher.Name.Should().BeEmpty();
    }

    [Theory]
    [InlineData("Penguin Books")]
    [InlineData("Random House")]
    [InlineData("HarperCollins")]
    [InlineData("Simon & Schuster")]
    [InlineData("Macmillan")]
    public void Name_ShouldBeSetCorrectly_WhenPublisherNameIsAssigned(string publisherName)
    {
        // Arrange
        var publisher = new Publisher();

        // Act
        publisher.Name = publisherName;

        // Assert
        publisher.Name.Should().Be(publisherName);
    }

    [Fact]
    public void Id_ShouldBeSetCorrectly_WhenIdIsAssigned()
    {
        // Arrange
        var publisher = new Publisher();
        var id = 25;

        // Act
        publisher.Id = id;

        // Assert
        publisher.Id.Should().Be(id);
    }

    [Fact]
    public void EmptyName_ShouldBeSetCorrectly_WhenEmptyNameIsAssigned()
    {
        // Arrange
        var publisher = new Publisher();

        // Act
        publisher.Name = "";

        // Assert
        publisher.Name.Should().BeEmpty();
    }

    [Fact]
    public void NullName_ShouldBeSetCorrectly_WhenNullNameIsAssigned()
    {
        // Arrange
        var publisher = new Publisher();

        // Act
        publisher.Name = null!;

        // Assert
        publisher.Name.Should().BeNull();
    }

    [Fact]
    public void LongName_ShouldBeSetCorrectly_WhenLongPublisherNameIsAssigned()
    {
        // Arrange
        var publisher = new Publisher();
        var longName = "This is a very long publisher name that might be used in real scenarios";

        // Act
        publisher.Name = longName;

        // Assert
        publisher.Name.Should().Be(longName);
    }
} 