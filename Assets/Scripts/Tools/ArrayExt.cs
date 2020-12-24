
using System;

namespace Tools
{
    public static class ArrayExt
    {
        public static void ForEach<T>(this Array array, Action<T> action)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (action == null)
                throw new ArgumentNullException("action");

            foreach (var item in array)
                action((T)item);
        }
    }
}
