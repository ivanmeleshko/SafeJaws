using System;
using System.Collections;
using cakeslice;
using UnityEngine;

namespace Scripts.UITools
{
    public class AlphaTween : MonoBehaviour
    {
        [SerializeField] private OutlineEffect _outlineEffect;
        [SerializeField] private float _moveDuration = 1f;
        public static Action OnTweenColorAction;

        public void OnEnable()
        {
            OnTweenColorAction += OnTweenColorActionMethod;
        }

        public void OnDisable()
        {
            OnTweenColorAction -= OnTweenColorActionMethod;
        }

        private Coroutine _coroutina;
        private void OnTweenColorActionMethod()
        {
            if(_coroutina != null) StopCoroutine(_coroutina);
            _coroutina = StartCoroutine(MoveCoroutine());
        }

        private IEnumerator MoveCoroutine()
        {
            float t = 0.0f;
            float percent = 0;
            Color start = _outlineEffect.lineColor1;
            start.a = 0;
            _outlineEffect.lineColor1 = start;
            while (t < _moveDuration)
            {
                t += Time.deltaTime;
                percent = t / _moveDuration;
                start.a = Mathf.Lerp(0f, 1f, percent );
                _outlineEffect.lineColor1 = start;
                yield return null;
            }
            start.a = 1;
            yield return new WaitForSeconds(0.7f);
            percent =0f;
            t = 0;
            while (t < _moveDuration)
            {
                t += Time.deltaTime;
                percent = t / _moveDuration;
                start.a = Mathf.Lerp(1f, 0f, percent);
                _outlineEffect.lineColor1 = start;
                yield return null;
            }
            start.a = 0;
            _outlineEffect.lineColor1 = start;
        }
    }
}
