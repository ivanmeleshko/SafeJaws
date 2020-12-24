
using System;
using System.Collections;
using UnityEngine;

namespace Screens
{
    public class LoadingScreen : BaseScreen
    {
        public override void Show(Action onComplete)
        {
            base.Show(onComplete);
            StartCoroutine(WaitAndStart());
        }

        private IEnumerator WaitAndStart()
        {
            //yield return new WaitForEndOfFrame();
            //yield return new WaitForEndOfFrame();
            //yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            CompleteScreen();
        }
    }
}