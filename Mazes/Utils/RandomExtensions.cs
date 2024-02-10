namespace Mazes.Utils;

public static class RandomExtensions
{
    public static T? Sample<T>(this Random random, IReadOnlyList<T> collection)
    {
        return collection.Any() ? collection[random.Next(collection.Count)] : default;
    }
}