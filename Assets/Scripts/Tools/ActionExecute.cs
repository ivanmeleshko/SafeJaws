
using System;

namespace Tools
{
    public static class ActionExecute
    {
        public static void Execute(this Action action)
        {
            if (action != null) action();
        }

        public static void Execute<T>(this Action<T> action, T method)
        {
            if (action != null) action(method);
        }

        public static void Execute<T, K>(this Action<T, K> action, T method, K methodK)
        {
            if (action != null) action(method, methodK);
        }
    }
}
