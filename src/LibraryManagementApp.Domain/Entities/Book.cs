namespace LibraryManagementApp.Domain.Entities;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int PublicationYear { get; set; }
    public int NumberOfCopies { get; set; }

    public int PublisherId { get; set; }
    public Publisher Publisher { get; set; } = null!;

    public ICollection<Author> Authors { get; set; } = new List<Author>();
    public ICollection<Category> Categories { get; set; } = new List<Category>();
} 