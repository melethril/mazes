namespace Mazes.Core;

public sealed class CellAttribute(CellAttributeType type, object? value = default)
{
    public CellAttributeType Type { get; } = type;
    public object? Value { get; } = value;

    public T? GetValueAs<T>()
    {
        return Value switch
        {
            null => default,
            T t => t,
            _ => throw new InvalidOperationException($"Attribute value was expected to be of type {typeof(T).Name} but was actually a {Value.GetType().Name}")
        };
    }
}