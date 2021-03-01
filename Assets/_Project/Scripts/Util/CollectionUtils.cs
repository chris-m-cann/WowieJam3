using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Util
{
    public static class CollectionUtils
    {
        public static void DeleteRange<T>(ref T[] self, int fromInclusive, int toExclusive)
        {
            var diff = toExclusive - fromInclusive;

            for (int i = fromInclusive; i < self.Length - diff; i++)
            {
                // moving elements downwards, to fill the gap at [index]
                self[i] = self[i + diff];
            }
            Array.Resize(ref self, self.Length - diff);
        }

        public static T RandomElement<T>(this T[] self)
        {
            return self[Random.Range(0, self.Length)];
        }

        public static T RandomElement<T>(this List<T> self)
        {
            return self[Random.Range(0, self.Count)];
        }


        public static void AddNullableRange<T>(this List<T> self, IEnumerable<T> range)
        {
            if (range == null) return;

            self.AddRange(range);
        }

        public static Pair<T1, T2>[] ToPairArray<T1, T2>(this Dictionary<T1, T2> self)
        {
            var array = new Pair<T1, T2>[self.Count];
            KeyValuePair<T1, T2> pair;
            using var enumerator = self.GetEnumerator();
            enumerator.MoveNext();
            for (int i = 0; i < self.Count; i++)
            {
                pair = enumerator.Current;
                array[i] = new Pair<T1, T2>
                {
                    Item1 = pair.Key,
                    Item2 = pair.Value
                };
                enumerator.MoveNext();
            }

            return array;
        }

        public static Dictionary<T1, T2> ToDictionary<T1, T2>(this Pair<T1, T2>[] self)
        {
            var dictionary = new Dictionary<T1, T2>(self.Length);
            foreach (var pair in self)
            {
                dictionary.Add(pair.Key, pair.Value);
            }
            return dictionary;
        }
    }
}