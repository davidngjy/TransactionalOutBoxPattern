using System.Reflection;

namespace TransactionalOutBoxPattern.Domain;

public abstract class Enumeration<TEnum> : IEquatable<Enumeration<TEnum>>
    where TEnum : Enumeration<TEnum>
{
    private static readonly Dictionary<int, TEnum> Enumerations = GetEnumerations();

    public string Name { get; }
    public int Value { get; }

    protected Enumeration(string name, int value)
    {
        Name = name;
        Value = value;
    }

    public static bool ContainName(string name) =>
        Enumerations.Values.Any(x => x.Name == name);

    public static bool ContainValue(int value) =>
        Enumerations.ContainsKey(value);

    public static TEnum FromName(string name)
    {
        var enumeration = Enumerations
            .Values
            .FirstOrDefault(x => x.Name == name);

        if (enumeration is null)
            throw new ArgumentException($"{name} does not exists {typeof(TEnum)}.", nameof(name));

        return enumeration;
    }

    public static TEnum FromValue(int value)
    {
        if (Enumerations.ContainsKey(value))
            return Enumerations[value];

        throw new ArgumentException($"{value} does not exists in {typeof(TEnum)}.", nameof(value));
    }

    public bool Equals(Enumeration<TEnum>? other)
    {
        if (ReferenceEquals(null, other))
            return false;

        return GetType() == other.GetType() &&
               Value == other.Value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Enumeration<TEnum> enumeration)
            return Equals(enumeration);

        return false;
    }

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Name;

    private static Dictionary<int, TEnum> GetEnumerations()
    {
        var enumerationType = typeof(TEnum);

        var fieldsForType = enumerationType
            .GetFields(
                BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.FlattenHierarchy
            )
            .Where(fieldInfo => enumerationType.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fieldInfo => (TEnum)fieldInfo.GetValue(default)!);

        return fieldsForType.ToDictionary(x => x.Value);
    }
}
