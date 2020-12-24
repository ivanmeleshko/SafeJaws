
using Enum;
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Networking;
using Model;
using Managers;

namespace Screens
{
    [Serializable]
    public class OneSection
    {
        public string color;
        public string text;
        public string textColor;
        public string font;
        public int priceVar;
        public bool fullContact;
    }

    public class TwoSection
    {
        public string left;
        public string right;
        public string text;
        public string textColor;
        public string font;
        public int priceVar;
        public bool fullContact;
    }

    public class ThreeSection
    {
        public string left;
        public string middle;
        public string right;
        public string text;
        public string textColor;
        public string font;
        public int priceVar;
        public bool fullContact;
    }
    public class PopularObject
    {
        public string uuid;
        public int priceVar;
        public bool fullContact;
    }

    public class AddChartPopup : BaseScreen
    {  
        [SerializeField] private ModelColorScreen _modelColorScreen;
        [SerializeField] private string _fileServer;
        [SerializeField] private string _userName;
        [SerializeField] private string _userPassword;

        [DllImport("__Internal")] private static extern void addPredefineProductToChart(string details);

        [DllImport("__Internal")] private static extern void addCustomProductOnCart(string details, string img1, string img2, string img3);

        public void OnClickSend()
        {
            Hide();
            if (ModelController.Instance.IsPopular)
            {
                AddPredefineProductToChart();
            }
            else
            {
                int priceV;
                if (GameManager.Instance._customText) // use text
                {
                    if (GameManager.Instance.customImage[0] || GameManager.Instance.customImage[2])
                    {
                        if(GameManager.Instance.StartPrice == 0)
                            priceV = 7;     //With Image and Text Bought
                        else
                            priceV = 3;     //With Image and Text
                    }
                    else
                    {
                        if (GameManager.Instance.StartPrice == 0)
                            priceV = 6;//With Text Bought
                        else
                            priceV = 2;//With Text
                    }
                }
                else if (GameManager.Instance.customImage[0] || GameManager.Instance.customImage[1] ||
                         GameManager.Instance.customImage[2])
                {
                    if (GameManager.Instance.StartPrice == 0)
                        priceV = 5;     //With Image Bought
                    else
                        priceV = 1;     //With Image
                }
                else
                {
                    if (GameManager.Instance.StartPrice == 0)
                        priceV = 4;     //DefaultBougth
                    else
                        priceV = 0;     //Default
                }
                Debug.LogError(priceV);
                bool Full = UIManager.FullContact;
                string[] Images = new string[3];

                for (int i = 0; i < Images.Length; i++)
                {
                    if (_modelColorScreen.Images[i].MainTexture2D != null)
                    {
                        byte[] textureByte = ImageConversion.EncodeToPNG(_modelColorScreen.Images[i].MainTexture2D);
                        Images[i] = Convert.ToBase64String(textureByte);
                    }
                    else
                        Images[i] = "";
                }


                TwoSection object2 = new TwoSection
                {
                    left = "Red",
                    right = "Red",
                    text = "",
                    textColor = "",
                    font = "",
                    priceVar = priceV,
                    fullContact = Full
                };


                ThreeSection object3 = new ThreeSection
                {
                    left = "Red",
                    middle = "Red",
                    right = "Red",
                    text = "",
                    textColor = "",
                    font = "",
                    priceVar = priceV,
                    fullContact = Full
                };

                OneSection object1 = new OneSection
                {
                    color = "Red",
                    text = "",
                    textColor = "",
                    font = "",
                    priceVar = priceV,
                    fullContact = Full
                };

                if (ScreenManager.Instance.ModelColorScreen.Images[1].MainTexture2D != null)
                {
                    if (ScreenManager.Instance.ModelColorScreen.Images[1].ImageType == Enum.ImageType.TextImage)
                    {
                        object1.text = ScreenManager.Instance.ModelColorScreen.Images[1].ImageText.ToUpper();
                        object2.text = ScreenManager.Instance.ModelColorScreen.Images[1].ImageText.ToUpper();
                        object3.text = ScreenManager.Instance.ModelColorScreen.Images[1].ImageText.ToUpper();
                        object3.textColor = object2.textColor = object1.textColor = ScreenManager.Instance.ModelColorScreen.Images[1].FontModel.FontColor.ToString();
                        object3.font = object2.font = object1.font = ScreenManager.Instance.ModelColorScreen.Images[1].FontModel.Font.name.ToString();
                    }
                }

                if (Managers.GameManager.Instance.selectedModelType == ModelType.OneColor)
                {
                    object1.color = Managers.GameManager.Instance.colorSelected[0].ToString();
                    Debug.Log("UNITY" + JsonUtility.ToJson(object1));
#if UNITY_WEBGL && !UNITY_EDITOR
                    addCustomProductOnCart(JsonUtility.ToJson(object1), Images[0], Images[1], Images[2]);
#endif
                }
                else if (Managers.GameManager.Instance.selectedModelType == ModelType.TwoColor)
                {
                    object2.left = Managers.GameManager.Instance.colorSelected[0].ToString();
                    object2.right = Managers.GameManager.Instance.colorSelected[1].ToString();
                    Debug.Log("UNITY" + JsonUtility.ToJson(object2));
#if UNITY_WEBGL && !UNITY_EDITOR
                    addCustomProductOnCart(JsonUtility.ToJson(object2), Images[0], Images[1], Images[2]);
#endif
                }
                else if (Managers.GameManager.Instance.selectedModelType == ModelType.ThirdColor)
                {
                    object3.left = Managers.GameManager.Instance.colorSelected[0].ToString();
                    object3.right = Managers.GameManager.Instance.colorSelected[1].ToString();
                    object3.middle = Managers.GameManager.Instance.colorSelected[2].ToString();
                    Debug.Log("UNITY" + JsonUtility.ToJson(object3));
#if UNITY_WEBGL && !UNITY_EDITOR
                    addCustomProductOnCart(JsonUtility.ToJson(object3), Images[0], Images[1], Images[2]);
#endif
                }

                foreach (var item in Images)
                {
                    Debug.Log(item);
                }

                //Application.OpenURL("https://www.safejawz.com/cart");
            }
        }

        private void AddPredefineProductToChart()
        {
            Debug.Log("AddPredefineProductToChart");
            bool Full = UIManager.FullContact;
            string productId = ModelController.Instance.GetCurrentProductId();
            int isFree = GameManager.Instance.StartPrice == 0 ? 0 : 1;

            PopularObject object3 = new PopularObject
            {
                uuid = productId,
                fullContact = Full,
                priceVar = isFree
            };
#if UNITY_WEBGL && !UNITY_EDITOR
            addPredefineProductToChart(JsonUtility.ToJson(object3));
#endif
            }

        public void OnSendAndReset()
        {
            Hide();
            _modelColorScreen.Show(null);
        }

        public string UploadFile(Texture2D texture, string UploadDirectory = "")
        {
            string PureFileName = "Test3";
            String uploadUrl = String.Format("{0}{1}/{2}", _fileServer, UploadDirectory, PureFileName);
            WebRequest req = (WebRequest) WebRequest.Create(uploadUrl);
            req.Proxy = null;
            req.Method = WebRequestMethods.Ftp.UploadFile;
            req.Credentials = new NetworkCredential(_userName, _userPassword);

            byte[] data = texture.GetRawTextureData();

            req.ContentLength = data.Length;
            Stream stream = req.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Close();
            FtpWebResponse res = (FtpWebResponse) req.GetResponse();
            return res.StatusDescription;
        }

        IEnumerator Save(Texture2D texture, string UploadDirectory = "")
        {
            byte[] myData = texture.GetRawTextureData();
            string PureFileName = "Test4";
            String uploadUrl = String.Format("{0}{1}/{2}", _fileServer, UploadDirectory, PureFileName);
            UnityWebRequest www = UnityWebRequest.Put(uploadUrl, myData);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Upload complete!");
            }
        }

        public void Saves(string texture, string UploadDirectory = "")
        {
            String FilePath = texture;
            Debug.Log("Path: " + FilePath);
            string PureFileName = "Test4";
            String uploadUrl = String.Format("{0}{1}/{2}", _fileServer, UploadDirectory, PureFileName);
            WebClient client = new System.Net.WebClient();
            Uri uri = new Uri(uploadUrl);

            client.UploadProgressChanged += new UploadProgressChangedEventHandler(OnFileUploadProgressChanged);
            client.UploadFileCompleted += new UploadFileCompletedEventHandler(OnFileUploadCompleted);
            client.Credentials = new System.Net.NetworkCredential(_userName, _userPassword);
            client.UploadFileAsync(uri, "STOR", FilePath);

        }

        void OnFileUploadProgressChanged(object sender, UploadProgressChangedEventArgs e)
        {
            Debug.Log("Uploading Progreess: " + e.ProgressPercentage);
        }

        void OnFileUploadCompleted(object sender, UploadFileCompletedEventArgs e)
        {
            Debug.Log("File Uploaded");
        }

        public void OnCloseREportProblem()
        {
            gameObject.SetActive(false);
        }
    }
}