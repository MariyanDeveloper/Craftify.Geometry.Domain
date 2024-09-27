namespace Craftify.Geometry.Domain;

public static class ReadOnlyList
{
    public static IReadOnlyList<T> Of<T>() => [];

    public static IReadOnlyList<T> Of<T>(T value) => [value];

    public static IReadOnlyList<T> Of<T>(IEnumerable<T> values) => [.. values];

    public static IReadOnlyList<T> Of<T>(IEnumerable<T> range1, IEnumerable<T> range2) =>
        [.. range1, .. range2];
}
