

using UnityEngine;
using UnityEngine.UI;

public class BuildNumber : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private string _numberOfBuild;

    public void OnEnable()
    {
#if Test_build
        //_text.text =  Application.version;
        
        _text.text =  Application.buildGUID;
#endif
    }
}