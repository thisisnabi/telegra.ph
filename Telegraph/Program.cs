var builder = WebApplication.CreateBuilder(args);

builder.ConfigureDbContext();

builder.Services.AddScoped<LetterService>();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "Telegraph";
});

builder.Services.AddHybridCache();

var app = builder.Build();

app.UseHttpsRedirection();
 
app.MapPost("/letters", async (LetterService service, CreateLetterRequest request) =>
{
    var response = await service.CreateAsync(request);
    return Results.Created($"/letters/{response.Slug}", response);
});


app.MapPut("/letters/{slug}", async (LetterService service, string slug, UpdateLetterRequest request) =>
{
    var response = await service.UpdateAsync(slug, request);
    return Results.Ok(response);
});


app.MapGet("/letters/{slug}", async (LetterService service, string slug, CancellationToken cancellationToken) =>
{
    var response = await service.GetAsync(slug, cancellationToken);
    return Results.Ok(response);
});

app.Run();
