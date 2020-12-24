using System;
using Popups;

namespace Tools
{
    public static class BasePopupExt
    {
        public static T GetPopup<T>(this BasePopup popup) where T : BasePopup
        {
            if (popup is T)
            {
                return (popup as T);
            }
            throw new Exception("Get popup incorrect type set");
        }
    }
}
