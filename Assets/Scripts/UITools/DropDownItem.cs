using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Screens;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UITools
{
    public class DropDownItem : MonoBehaviour
    {
        [SerializeField] private FontModel[] _fontModels;
        [SerializeField] private Text _text;
        private bool _applyFont = false;
        private void OnEnable()
        {
            Font font;
            if (GetFont(out font))
            {
                _text.font = font;
                _applyFont = true;
            }
            else
            {
                _applyFont = false;
            }
        }
        
        private bool GetFont(out Font font)
        {
            font = null;
            FontModel fontodel = _fontModels.ToList().Find(p => p.GetName().Equals(_text.text));
            if (fontodel != null)
            {
                font = fontodel.Font;
                return true;
            }

            return false;
        }

        private void FixedUpdate()
        {
            if(!_applyFont)
                OnEnable();
        }
        //public ModelColorScreen asd;
        //[ContextMenu("Test")]
        //public void RUpdate()
        //{
        //    _fontModels = asd.FontModels;
        //}
    }
}
