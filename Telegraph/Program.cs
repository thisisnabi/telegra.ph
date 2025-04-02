var builder = WebApplication.CreateBuilder(args);

builder.ConfigureDbContext();

builder.Services.AddScoped<LetterService>();

var app = builder.Build();

app.UseHttpsRedirection();
 
app.MapPost("/letters", async (LetterService service, CreateLetterRequest request) =>
{
    var response = await service.CreateLetterAsync(request);
    return Results.Created($"/letters/{response.Slug}", response);
});

app.MapGet("/letters/{slug}", async (LetterService service, string slug) =>
{
    var response = await service.GetLetterAsync(slug);
    return Results.Ok(response);
});

app.Run();
