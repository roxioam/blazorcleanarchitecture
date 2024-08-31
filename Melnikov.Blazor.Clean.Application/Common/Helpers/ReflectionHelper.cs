namespace Melnikov.Blazor.Clean.Application.Common.Helpers;

public static class ReflectionHelper
{
    public static List<Type> GetInheritedClasses(Type type)
    {
        return AppDomain.CurrentDomain
            .GetAssemblies()  // Get all assemblies in the current application domain
            .SelectMany(assembly => assembly.GetTypes())  // Get all types in each assembly
            .Where(t => t is { IsClass: true, IsAbstract: false } && t.IsSubclassOf(type))  // Filter for derived classes
            .ToList();
    }
}