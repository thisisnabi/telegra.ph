namespace Telegraph;

public interface IAssemblyMarker { }

public static class AssemblyMarker
{
    private static readonly Lazy<Assembly> _currentAssembly =
        new Lazy<Assembly>(() => typeof(IAssemblyMarker).Assembly);

    public static Assembly ApplicationAssembly => _currentAssembly.Value;
}
