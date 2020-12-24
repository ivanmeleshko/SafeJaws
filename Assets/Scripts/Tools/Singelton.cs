using UnityEngine;

namespace Tools
{
    public class Singelton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        private static object _lock = new object();

        public static T Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        Debug.LogErrorFormat("Instance not greate yet. Fix this. type of : {0}", typeof(T));
                    }

                    return _instance;
                }
            }
            private set { _instance = value; }
        }
        
        protected void Awake()
        {
            _instance = this as T;
            if (this.transform.root == this.transform)
            {
                DontDestroyOnLoad(this.gameObject);
            }
        }

        private void OnDestroy()
        {
            _instance = null;
        }
    }
}