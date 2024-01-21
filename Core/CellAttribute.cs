namespace Mazes
{
    public sealed class CellAttribute(CellAttributeType type, object? value = default)
    {
        public CellAttributeType Type { get; init; } = type;
        public object? Value { get; } = value;

        public T? GetValueAs<T>()
        {
            if (Value == null) return default;
            if (Value is T t) return t;

            throw new InvalidOperationException(
                $"Attribute value was expected to be of type {typeof(T).Name} but was actually a {Value.GetType()?.Name}");
        }
    }
}