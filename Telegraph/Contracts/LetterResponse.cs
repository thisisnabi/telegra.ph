namespace Telegraph.Contracts;

public record LetterResponse(string Title, string Author, string Content, DateTime CreatedAtLocal);