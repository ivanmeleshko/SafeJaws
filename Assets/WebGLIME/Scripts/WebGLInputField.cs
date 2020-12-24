using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Runtime.InteropServices;
using System;

public class WebGLInputField : InputField
{
#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern void ShowInputFieldDialog(string text);
    [DllImport("__Internal")]
    private static extern void HideInputFieldDialog();
    [DllImport("__Internal")]
    private static extern bool IsInputFieldDialogActive();
    [DllImport("__Internal")]
    private static extern string GetInputFieldValue();
    [DllImport("__Internal")]
    private static extern int GetInputFieldCursortPosition();
    [DllImport("__Internal")]
    private static extern int GetInputFieldCursortFocusPosition();
    [DllImport("__Internal")]
    private static extern void SetInputFieldCursortPosition(int selectionStart, int selectionEnd);
    private bool captureAllKeyboardInput
    {
        get
        {
            return WebGLInput.captureAllKeyboardInput;
        }
        set
        {
            WebGLInput.captureAllKeyboardInput = value;
        }
    }
    private float timer;
    private Coroutine overlayhtml;
    private Coroutine setposCoroutine;

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        captureAllKeyboardInput = false;

        ShowInputFieldDialog(text);

        if (IsInputFieldDialogActive() && overlayhtml != null)
        {
            if(setposCoroutine != null)
            {
                SetSelection();
            }
            else
            {
                setposCoroutine = StartCoroutine(DelySetPostion());
            }
        }
        else
        {
            overlayhtml = StartCoroutine(this.OverlayHtmlCoroutine());
        }
    }

    private IEnumerator DelySetPostion()
    {
        captureAllKeyboardInput = true;
        yield return null;
        SetSelection();
        captureAllKeyboardInput = false;
        setposCoroutine = null;
        System.GC.Collect();
    }
    
    private IEnumerator OverlayHtmlCoroutine()
    {
        yield return DelySetPostion();
        while (IsInputFieldDialogActive() && isFocused)
        {
            yield return null;
            var textFromHtml = GetInputFieldValue();
            if (textFromHtml != this.text)
            {
                this.text = textFromHtml;
                ForceLabelUpdate();
                yield return null;
            }

            if (!captureAllKeyboardInput && setposCoroutine == null && !Input.GetMouseButton(0))
            {
                UpdateCaretPositions();
                yield return null;
            }
         //   updateMobile();
        }
        HideInputFieldDialog();
        EventSystem.current.SetSelectedGameObject(null);
        captureAllKeyboardInput = true;
        overlayhtml = null;
        System.GC.Collect();
    }

    private void SetSelection()
    {
        //caretPosition = textFromHtml.Length;
        var selectionStart = selectionAnchorPosition < selectionFocusPosition ? selectionAnchorPosition : selectionFocusPosition;
        var selectionEnd = selectionAnchorPosition > selectionFocusPosition ? selectionAnchorPosition : selectionFocusPosition;
        SetInputFieldCursortPosition(selectionStart, selectionEnd);
    }

    private void UpdateCaretPositions()
    {
        var cpos = GetInputFieldCursortPosition();
        var fpos = GetInputFieldCursortFocusPosition();
        var changed = false;
        if (cpos != caretPosition)
        {
            caretPosition = cpos;
            changed = true;
        }
        if (fpos != selectionFocusPosition)
        {
            selectionFocusPosition = fpos;
            changed = true;
        }

        if (changed)
        {
            ForceLabelUpdate();
        }
    }

#endif
}