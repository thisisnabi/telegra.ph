namespace Telegraph.Services;

public class LetterService
{
    private readonly TelegraphDbContext _dbContext;
    public LetterService(TelegraphDbContext dbContext)
    {
        _dbContext = dbContext;
    }
     
    public async Task<CreateLetterResponse> CreateLetterAsync(CreateLetterRequest request)
    {
        var letter = Letter.Create(request.Title, request.Author, request.Content);

        _dbContext.Letters.Add(letter);
        await _dbContext.SaveChangesAsync();

        return new CreateLetterResponse(letter.Slug);
    }

    public async Task<LetterResponse> GetLetterAsync(string slug)
    {
        var letter = await _dbContext.Letters.FindAsync(slug);
        if (letter is null)
           throw new KeyNotFoundException();

        return new LetterResponse(letter.Title, letter.Author, letter.Content, letter.CreatedAtUtc.ToLocalTime());
    }

}
