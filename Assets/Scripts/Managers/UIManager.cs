using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class UIManager : MonoBehaviour
{
    [SerializeField] private string _homepage = "https://www.safejawz.com/";
    [SerializeField] private Button _fullContact;
    [SerializeField] private Button _semiContact;
    public static bool FullContact = true;

    [DllImport("__Internal")]
    private static extern void openMyUrl(string myurl, bool inNewTab);

    public void OpenHomepage()
    {
        Application.OpenURL(_homepage);
    }

    public void OpenUrl(string link)
    {
#if UNITY_EDITOR
        Application.OpenURL(link);
#endif
        openMyUrl(link, true);
    }

    public void OpenUrlInSameTab(string link)
    {
        openMyUrl(link, false);
    }

    public void ChooseFullContact()
    {
        _fullContact.image.color += new Color(0, 0, 0, 0.5f);
        _semiContact.image.color -= new Color(0, 0, 0, 0.5f);

        _fullContact.interactable = false;
        _semiContact.interactable = true;

        FullContact = true;
    }

    public void ChooseSemiContact()
    {
        _fullContact.image.color -= new Color(0, 0, 0, 0.5f);
        _semiContact.image.color += new Color(0, 0, 0, 0.5f);

        _fullContact.interactable = true;
        _semiContact.interactable = false;

        FullContact = false;
    }
}
