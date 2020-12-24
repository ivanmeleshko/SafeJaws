using System;
using UnityEngine;

public class JsonHelper
{
    public static T[] getJsonArray<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.values;
    }

    public static string arrayToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.values = array;
        return JsonUtility.ToJson(wrapper);
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] values;
    }
}
