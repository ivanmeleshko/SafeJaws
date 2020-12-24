using Enum;
using UnityEngine;
using UnityEngine.UI;

public class TweenColor : MonoBehaviour
{
    [SerializeField] private TweenType _tweenType;
    [SerializeField] private Image targetGraphic;
    [SerializeField] private Color _from;
    [SerializeField] private Color _to;
    [SerializeField] private bool _alphaOnly;

    private float _duration = 1f;
    private Graphic[] _graphics;
    private float _durationCache;
    private bool _pingPongUp = true;
    private bool _work = false;
    private Color _colorCache;

    protected Graphic[] Graphics
    {
        get
        {
            if (_graphics == null)
            {
                _graphics = targetGraphic.transform.GetComponentsInChildren<Graphic>();
            }

            return _graphics;
        }
    }

    [ContextMenu("StartTween")]
    private void StartTween()
    {
        StartTween(_from, _to, _duration);
    }

    public void StartTween(float time)
    {
        StartTween(_from, _to, time);
    }

    public void StartTween(Color from, Color to, float time)
    {
        _from = from;
        _to = to;
        _duration = time;
        _durationCache = 0;
        _pingPongUp = true;
        _work = true;
    }

    public void StopTween(Color resetColor)
    {
        _work = false;
        foreach (Graphic graphic in Graphics)
        {
            if (_alphaOnly)
            {
                _colorCache = graphic.color;
                _colorCache.a = resetColor.a;
            }
            else
                _colorCache = resetColor;
            graphic.color = _colorCache;
        }
    }

    [ContextMenu("Stop")]
    public void StopTween()
    {
        _work = false;
    }

    private void FixedUpdate()
    {
        if (_work)
        {
            foreach (Graphic graphic in Graphics)
            {
                if (_alphaOnly)
                {
                    _colorCache = graphic.color;
                    _colorCache.a = Color.Lerp(_from, _to, Mathf.PingPong(GetTime(), _duration)).a;
                }
                else
                    _colorCache = Color.Lerp(_from, _to, Mathf.PingPong(GetTime(), _duration));
                graphic.color = _colorCache;
            }
        }
    }

    private float GetTime()
    {
        if (_tweenType == TweenType.Once)
        {
            _durationCache += Time.deltaTime;
            if (_durationCache >= _duration)
            {
                _durationCache = 0;
                _work = false;
            }
        }

        if (_tweenType == TweenType.Loop)
        {
            _durationCache += Time.deltaTime;
            if (_durationCache >= _duration)
                _durationCache = 0;
        }

        if (_tweenType == TweenType.PingPong)
        {
            if (_pingPongUp)
            {
                _durationCache += Time.deltaTime;
                if (_durationCache >= _duration)
                {
                    _durationCache = _duration;
                    _pingPongUp = !_pingPongUp;
                }
            }
            else
            {
                _durationCache -= Time.deltaTime;
                if (_durationCache <= 0)
                {
                    _durationCache = 0;
                    _pingPongUp = !_pingPongUp;
                }
            }
        }

        return _durationCache;
    }
}