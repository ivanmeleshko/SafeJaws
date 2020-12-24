using UnityEngine;
using UnityEngine.UI;

namespace Popups
{
    public class ReportPoup : MonoBehaviour
    {
        [SerializeField] private InputField _textField;

        public void OnSend()
        {
            string email = "@gmail.com";
            string subject = MyEscapeURL("FEEDBACK/SUGGESTION");
            
            string body = MyEscapeURL(_textField.ToString()+"\n\n\n\n" +
                                      "________" +
                                      "\n\nPlease Do Not Modify This\n\n" +
                                      "Model: " + SystemInfo.deviceModel + "\n\n" +
                                      "OS: " + SystemInfo.operatingSystem + "\n\n" +
                                      "________");
            Application.OpenURL("mailto:" + email + "?subject=" + subject + "&body=" + body);
        }

        string MyEscapeURL(string url)
        {
            return WWW.EscapeURL(url).Replace("+", "%20");
        }

        public void OnCloseREportProblem()
        {
            gameObject.SetActive(false);
        }
    }
}
