
using UnityEngine;

namespace Utility
{
    public static class Tools
    {
        public static GameObject InstantiateChild(Canvas parent, GameObject prefab)
        {
            return InstantiateChild(parent.transform, prefab);
        }


        public static GameObject InstantiateChild(Transform parent, GameObject prefab)
        {
            if (prefab == null)
            {
                Debug.LogError("[UITools] AddChild empty prefab");
                return null;
            }
            var go = Object.Instantiate(prefab) as GameObject;
            SetParent(parent, go);
            return go;
        }

        public static void SetParent(Transform parent, GameObject go)
        {
            if (go != null && parent != null)
            {
                Transform t = go.transform;
                t.SetParent(parent, false);
                t.localPosition = Vector3.zero;
                t.localRotation = Quaternion.identity;
                t.localScale = Vector3.one;
                go.layer = parent.gameObject.layer;
            }
        }

        public static GameObject InstantiateChild(Transform parent, GameObject prefab, Vector3 pos)
        {
            if (prefab == null)
            {
                Debug.LogError("[UITools] AddChild empty prefab");
                return null;
            }
            var go = Object.Instantiate(prefab, pos, Quaternion.identity) as GameObject;

            SetParent(parent, go);
            return go;
        }

        public static T InstantiateChild<T>(Transform parent, GameObject prefab) where T : Component
        {
            return InstantiateChild(parent, prefab).GetComponent<T>();
        }

        public static T InstantiateChild<T>(Transform parent, T prefab) where T : Object
        {
            return Object.Instantiate<T>(prefab, parent, false);
        }

        public static T InstantiateChild<T>(Canvas parent, T prefab) where T : Object
        {
            return InstantiateChild<T>( parent.transform, prefab);
        }

        public static void DestroyAllChild(Transform parent)
        {
            foreach (Transform child in parent)
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }
}
