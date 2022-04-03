namespace Tea2D.Vulkan.Generator.CSharp.Utils;

public static class CSharpNamingHelper
{
    public const string UnsafeKeyword = "unsafe";
    public const string FixedKeyword = "fixed";
    public const string ObjectKeyword = "object";
    public const string EventKeyword = "event";

    private static readonly HashSet<string> ReservedKeyword = new(StringComparer.OrdinalIgnoreCase)
    {
        ObjectKeyword,
        EventKeyword
    };

    public static string NormalizeFiledName(string name)
    {
        return ReservedKeyword.Contains(name)
            ? '@' + name
            : name;
    }

    public static string NormalizeEnumItemName(string name)
    {
        var parts = name.ToLower()
            .Split('_')
            .Select(p => char.ToUpper(p[0]) + p.Substring(1));

        return string.Concat(parts);
    }
}