
namespace Telegraph.DomainModels;

public sealed class Letter
{
    public string Title { get; private set; } = null!;

    public string Author { get; private set; } = null!;

    public string Slug { get; private set; } = null!;

    public string Content { get; private set; } = null!;

    public DateTime CreatedAtUtc { get; private set; }

    public static Letter Create(string title, string author, string content)
    {
        return new Letter
        {
            Title = title,
            Author = author,
            Slug = title.Kebaberize(),
            Content = content,
            CreatedAtUtc = DateTime.UtcNow
        };
    }

    internal void Update(string content)
    {
        Content = content;
    }
}
 
public sealed class LetterEntityConfiguration : IEntityTypeConfiguration<Letter>
{
    public void Configure(EntityTypeBuilder<Letter> builder)
    {
        builder.HasKey(l => l.Slug);

        builder.Property(l => l.Content)
               .HasColumnType("text");

        builder.Property(l => l.CreatedAtUtc)
               .IsRequired();

        builder.Property(l => l.Title)
               .HasMaxLength(255)
               .IsRequired();

        builder.Property(l => l.Author)
               .HasMaxLength(255)
               .IsRequired();
    }
}
