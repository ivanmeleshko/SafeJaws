
using System;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

namespace UITools
{
    public class PriceUpdater : MonoBehaviour
    {

        [DllImport("__Internal")]
        private static extern int onPriceChange(float value);

        [SerializeField] private Text _priceText;

        private void OnEnable()
        {
            GameManager.OnCostCnage += OnCostCnage;
        }

        private void OnDisable()
        {
            GameManager.OnCostCnage -= OnCostCnage;
        }

        private void OnCostCnage(float price)
        {
            //if (Model.ModelController.Instance.selectedPopular)
            //    price = 49.99f;
#if UNITY_WEBGL && !UNITY_EDITOR
            onPriceChange(price);
#endif
            if(price < 10)
                _priceText.text = String.Format("£{0:0.00}", price);
            else
                _priceText.text = String.Format("£{0:0.00}", price);
        }
    }
}
