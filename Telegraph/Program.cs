var builder = WebApplication.CreateBuilder(args);

builder.ConfigureDbContext();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();
 

app.Run();
