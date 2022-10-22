using System.Reflection;
using System.Runtime.CompilerServices;

namespace TransactionalOutBoxPattern.Domain;

public abstract class Enumeration<TEnum, TValue> :
    IEquatable<Enumeration<TEnum, TValue>>,
    IComparable<Enumeration<TEnum, TValue>>
    where TEnum : Enumeration<TEnum, TValue>
    where TValue : IEquatable<TValue>, IComparable<TValue>
{
    public string Name { get; }
    public TValue Value { get; }

    protected Enumeration(string name, TValue value)
    {
        Name = name;
        Value = value;
    }

    private static readonly Lazy<TEnum[]> EnumOptions = new(GetAllOptions, LazyThreadSafetyMode.ExecutionAndPublication);

    private static readonly Lazy<Dictionary<string, TEnum>> Names =
        new(() => EnumOptions.Value.ToDictionary(item => item.Name, StringComparer.OrdinalIgnoreCase));

    private static readonly Lazy<Dictionary<TValue, TEnum>> Values =
        new(() =>
        {
            var dictionary = new Dictionary<TValue, TEnum>();
            foreach (var item in EnumOptions.Value)
            {
                if (!dictionary.ContainsKey(item.Value))
                    dictionary.Add(item.Value, item);
            }

            return dictionary;
        });

    private static TEnum[] GetAllOptions()
        => typeof(TEnum).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            .Select(f => f.GetValue(null))
            .Cast<TEnum>()
            .ToArray();

    public static IReadOnlyList<TEnum> List
        => Names.Value.Values
            .ToList()
            .AsReadOnly();

    public static TEnum FromName(string name, bool ignoreCase = true)
    {
        if (string.IsNullOrEmpty(name))
            throw new ArgumentNullException(nameof(name));

        if (!Names.Value.TryGetValue(name, out var result))
            throw new ArgumentOutOfRangeException(nameof(name));

        return result;
    }

    public static bool TryFromName(string name, out TEnum result)
    {
        if (string.IsNullOrEmpty(name))
        {
            result = default!;
            return false;
        }

        return Names.Value.TryGetValue(name, out result!);
    }

    public static TEnum FromValue(TValue value)
    {
        if (!Values.Value.TryGetValue(value, out var result))
            throw new ArgumentOutOfRangeException(nameof(value));

        return result;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TEnum FromValue(TValue value, TEnum defaultValue)
        => !Values.Value.TryGetValue(value, out var result)
            ? defaultValue
            : result;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryFromValue(TValue value, out TEnum result)
        => Values.Value.TryGetValue(value, out result!);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString()
        => Name;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode()
        => Value.GetHashCode();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override bool Equals(object? obj)
        => (obj is Enumeration<TEnum, TValue> other) && Equals(other);

    public virtual bool Equals(Enumeration<TEnum, TValue>? other)
    {
        if (ReferenceEquals(this, other))
            return true;

        if (other is null)
            return false;

        return Value.Equals(other.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Enumeration<TEnum, TValue> left, Enumeration<TEnum, TValue> right)
        => left.Equals(right);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Enumeration<TEnum, TValue> left, Enumeration<TEnum, TValue> right)
        => !(left == right);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual int CompareTo(Enumeration<TEnum, TValue>? other)
    {
        if (other is null)
            return Value.CompareTo(default);

        return Value.CompareTo(other.Value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <(Enumeration<TEnum, TValue> left, Enumeration<TEnum, TValue> right)
        => left.CompareTo(right) < 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator <=(Enumeration<TEnum, TValue> left, Enumeration<TEnum, TValue> right)
        => left.CompareTo(right) <= 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >(Enumeration<TEnum, TValue> left, Enumeration<TEnum, TValue> right)
        => left.CompareTo(right) > 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator >=(Enumeration<TEnum, TValue> left, Enumeration<TEnum, TValue> right)
        => left.CompareTo(right) >= 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator TValue(Enumeration<TEnum, TValue> @enum)
        => @enum.Value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator Enumeration<TEnum, TValue>(TValue value)
        => FromValue(value);
}