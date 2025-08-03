namespace LibraryManagementApp.Application.DTOs;

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int PublicationYear { get; set; }
    public int NumberOfCopies { get; set; }
    public int PublisherId { get; set; }
    public string PublisherName { get; set; } = string.Empty;
    public List<AuthorDto> Authors { get; set; } = new();
    public List<CategoryDto> Categories { get; set; } = new();
} 