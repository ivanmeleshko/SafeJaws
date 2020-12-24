
using System;
using System.Collections.Generic;

namespace Tools
{
    public static class DictionaryExt
    {
        public static void ForEach<TKey, TValue>(this Dictionary<TKey, TValue> dict, Action<TKey, TValue> action)
        {
            if (dict == null)
                throw new ArgumentNullException("dict");
            if (action == null)
                throw new ArgumentNullException("action");

            foreach (KeyValuePair<TKey, TValue> item in dict)
                action(item.Key, item.Value);
        }
    }
}