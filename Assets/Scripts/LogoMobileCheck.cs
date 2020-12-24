using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoMobileCheck : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private bool _test = false;

    private void Start()
    {
        if (Application.isMobilePlatform || _test)
            rectTransform.sizeDelta = new Vector2(185, 65);
        else
            rectTransform.sizeDelta = new Vector2(227, 65);
    }
}
