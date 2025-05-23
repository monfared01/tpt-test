﻿namespace MainTest.Framework.Extensions;

public static class IEnumerableExtenstions
{
    public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
    {
        foreach (T item in enumeration)
            action(item);
    }
}
