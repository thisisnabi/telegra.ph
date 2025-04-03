using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace Telegraph.Services;

public sealed class LetterService
{
    private readonly TelegraphDbContext _dbContext;
    private readonly HybridCache _cache;

    public LetterService(TelegraphDbContext dbContext, HybridCache cache)
    {
        _dbContext = dbContext;
        _cache = cache;
    }

    public async Task<CreateLetterResponse> CreateAsync(CreateLetterRequest request)
    {
        var letter = Letter.Create(request.Title, request.Author, request.Content);

        _dbContext.Letters.Add(letter);
        await _dbContext.SaveChangesAsync();

        return new CreateLetterResponse(letter.Slug);
    }

    public async Task<LetterResponse> UpdateAsync(string slug, UpdateLetterRequest request)
    {
        var letter = await _dbContext.Letters.FirstOrDefaultAsync(s => s.Slug == slug);
        if (letter is null)
            throw new KeyNotFoundException();

        letter.Update(request.Content);
        await _dbContext.SaveChangesAsync();
        await _cache.RemoveAsync(slug);

        return new LetterResponse(letter.Title, letter.Author, letter.Content, letter.CreatedAtUtc.ToLocalTime());
    }
     
    public async Task<LetterResponse?> GetAsync(string slug, CancellationToken token)
    { 
        return await _cache.GetOrCreateAsync(slug,
            async cancel => {

                var letter = await _dbContext.Letters.FindAsync(slug, cancel);
                if (letter is null)
                    throw new KeyNotFoundException();

                return new LetterResponse(letter.Title, letter.Author, letter.Content, letter.CreatedAtUtc.ToLocalTime());
            },
            cancellationToken: token
        );
    }
}
