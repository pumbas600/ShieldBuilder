using System;
using System.Collections.Generic;
using UnityEngine;

public static class Utilities
{
    public static T[] CombineArrays<T>(params T[][] arrays)
    {
        List<T> combinedList = new List<T>();

        foreach (T[] array in arrays)
        {
            foreach (T element in array)
            {
                combinedList.Add(element);
            }
        }

        return combinedList.ToArray();
    }

    public static T[] CombineTwoArrays<T>(T[] a, T[] b)
    {
        int aLength = a.Length;
        Array.Resize(ref a, a.Length + b.Length);
        Array.Copy(b, 0, a, aLength, b.Length);
        return a;
    }
}
