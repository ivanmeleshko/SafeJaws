
using System;
using System.Collections;
using System.Runtime.InteropServices;
using Model;
using Screens;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SFB;

[RequireComponent(typeof(Button))]
public class CanvasSampleOpenFileImage : MonoBehaviour, IPointerDownHandler
{
    public string Title = "";
    public string FileName = "";
    public string Directory = "";
    public string Extension = "";

    [SerializeField] private Action<int, string, Texture2D> _imageLoadEvent;
    [SerializeField] private Text _outputPatchText;

    private int _index;

    public void OnDescribeOnEvent(int index, Action<int,string, Texture2D> onDescribe)
    {
        _index = index;
        _imageLoadEvent = onDescribe;
    }

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void UploadFile(string id);

    public void OnPointerDown(PointerEventData eventData) 
    {
        UploadFile(gameObject.name);
    }

    public void OnFileUploaded(string url) 
    {
        StartCoroutine(OutputRoutine(url));
    }
#else

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        var paths = StandaloneFileBrowser.OpenFilePanel(Title, Directory, Extension, false);
        if (paths.Length > 0)
        {
           StartCoroutine(OutputRoutine(new System.Uri(paths[0]).AbsoluteUri));
          // Load(new System.Uri(paths[0]).AbsoluteUri);
        }
    }
#endif

    [SerializeField] private Vector2 _imageOffest = Vector2.zero;

    private IEnumerator OutputRoutine(string url)
    {
        _outputPatchText.text = url;
        Debug.Log("URL: " + url);
        var loader = new WWW(url);

        ScreenManager.Instance.OnShowLoadingPopup();
        while (!loader.isDone)
        {
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForEndOfFrame();
        ScreenManager.Instance.OnHideLoadingPopup();
        _imageLoadEvent?.Invoke(_index, url, loader.texture);
    }


}