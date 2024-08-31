using System.Reflection;
using Melnikov.Blazor.Clean.Application.Common.Helpers;

namespace Melnikov.Blazor.Clean.Application.Common.Permissions;

public abstract class PermissionBase
{
    private static List<Type>? _types;
    private static List<string>? _permissions;

    public static List<string> GetAllPermissions()
    {
        return _permissions ??= GetPermissionTypes()
            .SelectMany(t => t.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy))
            .Select(f => f.GetValue(null)).OfType<string>().Where(s => !string.IsNullOrWhiteSpace(s))
            .Distinct(StringComparer.InvariantCultureIgnoreCase).ToList();
    }

    public static List<Type> GetPermissionTypes()
    {
        return _types ??= ReflectionHelper.GetInheritedClasses(typeof(PermissionBase));
    }
}