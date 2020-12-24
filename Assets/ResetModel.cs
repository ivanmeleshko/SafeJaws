using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Screens;

public class ResetModel : MonoBehaviour
{

    [SerializeField] private Button _sections;
    [SerializeField] private Button _colors;
    [SerializeField] private Button _text;
    [SerializeField] private ModelColorScreen _modelColorScreen;

    public void Reset()
    {
        StartCoroutine(ResetCoroutina());
    }

    private IEnumerator ResetCoroutina()
    {
        ScreenManager.Instance.OnShowLoadingPopup();
        yield return new WaitForSeconds(0.5f);
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        _sections.onClick.Invoke();
        _colors.onClick.Invoke();
        _modelColorScreen.ResetImages();
        yield return new WaitForEndOfFrame();
    }
}
