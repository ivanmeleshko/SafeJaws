using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.InteropServices;

namespace WebGLKeyboard
{
    /// <summary>
    /// Controls the flow of opening the keyboard and adding the necessary components to input fields as scenes load
    /// </summary>
    public class KeyboardController : MonoBehaviour
    {
        public List<UnityEngine.UI.InputField> nativeInputs = new List<UnityEngine.UI.InputField>();
        public bool isKeyboardOpen = false;
        public UnityEngine.UI.InputField currentNativeInput;

        [DllImport("__Internal")]
        private static extern void OpenInputKeyboard(string str);
        [DllImport("__Internal")]
        private static extern void CloseInputKeyboard();
        
        //Just adds these functions references to avoid stripping
        [DllImport("__Internal")]
        private static extern void FixInputOnBlur();
        [DllImport("__Internal")]
        private static extern void FixInputUpdate();

        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
        private void Start()
        {
            PadronizeObjectName();
            OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
            DontDestroyOnLoad(gameObject);
        }
      
        private void PadronizeObjectName()
        {
            gameObject.name = "_WebGLKeyboard";
            gameObject.transform.SetParent(null);
        }
        
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            for (int x = 0; x < nativeInputs.Count; x++)
            {
                DetectInputFocus detect = nativeInputs[x].gameObject.AddComponent<DetectInputFocus>();
                detect.Initialize(this);
            }
        }
  
        /// <summary>
        /// Call the external javascript function to trigger the keyboard and link to the input field
        /// </summary>
        /// <param name="input"></param>
        public void FocusInput(UnityEngine.UI.InputField input)
        {
            isKeyboardOpen = true;
            currentNativeInput = input;
#if !UNITY_EDITOR
            OpenInputKeyboard(input.text);
#endif

#if !UNITY_EDITOR && UNITY_WEBGL
            UnityEngine.WebGLInput.captureAllKeyboardInput = false;
#endif
        }
#if USE_TMPRO
        /// <summary>
        /// Call the external javascript function to trigger the keyboard and link to the input field
        /// </summary>
        /// <param name="input"></param>
        public void FocusInput(TMPro.TMP_InputField input)
        {
            isKeyboardOpen = true;
            currentTmproInput = input;
            OpenInputKeyboard(input.text);

#if !UNITY_EDITOR && UNITY_WEBGL
            UnityEngine.WebGLInput.captureAllKeyboardInput = false;
#endif
        }
#endif
        /// <summary>
        /// Forces the keyboard to close and unfocus
        /// </summary>
        public void ForceClose()
        {
#if UNITY_EDITOR
            return;
#endif
            CloseInputKeyboard();
        }
        /// <summary>
        /// Clear the link to the open keyboard
        /// </summary>
        public void LoseFocus()
        {
            if (isKeyboardOpen == false)
                return;

            isKeyboardOpen = false;
            if (currentNativeInput != null)
            {
                currentNativeInput.DeactivateInputField();
                currentNativeInput = null;
            }
#if USE_TMPRO
            if(currentTmproInput != null)
            {
                currentTmproInput.DeactivateInputField();
                currentTmproInput = null;
            }
#endif

#if !UNITY_EDITOR && UNITY_WEBGL
            UnityEngine.WebGLInput.captureAllKeyboardInput = true;
#endif
        }
        /// <summary>
        /// Receives the string inputed in the keyboard
        /// </summary>
        /// <param name="value"></param>
        public void ReceiveInputChange(string value)
        {
            Debug.Log(value);

            //This shouldn't happen
            if (isKeyboardOpen == false)
                return;

            //Applies the new string to the input field
            if (currentNativeInput != null)
            {
                currentNativeInput.text = value;
            }
#if USE_TMPRO
            if (currentTmproInput != null)
            {
                currentTmproInput.text = value;
            }
#endif
        }
        /// <summary>
        /// Returns all objects of a type in a loaded scene
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> FindObjectsOfTypeInScene<T>(Scene scene)
        {
            List<T> results = new List<T>();
            if (scene.isLoaded)
            {
                var allGameObjects = scene.GetRootGameObjects();
                for (int x = 0; x < allGameObjects.Length; x++)
                {
                    var go = allGameObjects[x];
                    results.AddRange(go.GetComponentsInChildren<T>(true));
                }
            }
            return results;
        }
    }
}