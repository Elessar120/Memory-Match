using System;
using System.Collections.Generic;

public static class ListExtensions
{
    private static Random random = new Random();

    // Extension method to shuffle a list in place
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = random.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
}