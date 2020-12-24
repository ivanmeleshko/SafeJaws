
using System;
using System.Collections;
using UnityEngine;

namespace Tools
{
    public class InvokManager : MonoBehaviour
    {
        private static InvokManager _instance;

        private static InvokManager Instance
        {
            get
            {
                if (_instance != null) return _instance;
                var go = new GameObject("InvokManager");
                _instance = go.AddComponent<InvokManager>();
                DontDestroyOnLoad(go);
                return _instance;
            }
        }

        public static void RunCoroutine(IEnumerator corroutine)
        {
            Instance.StartCoroutine(corroutine);
        }

        public static void Execute(Action action, float delay)
        {
            Instance.ExecuteAction(action, delay);
        }

        public void ExecuteAction(Action action, float delay)
        {
            StartCoroutine(CorRun(action, delay));
        }

        private IEnumerator CorRun(Action action, float delay)
        {
            yield return new WaitForSeconds(delay);
            action.Execute();
        }

    }
}