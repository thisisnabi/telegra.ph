namespace Telegraph;

public static class Extensions
{
    public static void ConfigureDbContext(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString(TelegraphDbContext.ConnectionStringName);
        if (string.IsNullOrEmpty(connectionString))
            throw new InvalidOperationException("The database connection string for TelegraphDbContext is missing or empty.");

        builder.Services.AddDbContext<TelegraphDbContext>(options =>
        {
            options.UseSqlServer(connectionString,
                options => options.MigrationsHistoryTable("__EFMigrationsHistory", TelegraphDbContext.DatabaseDefaultSchema));
        });
    }
}
