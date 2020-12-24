using System;
using System.Collections;
using UnityEngine;

public class TurnTimer : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Only for recognition")]
    string _name;

    [SerializeField]
    int _secondsForTurn = 60;

    public event Action<int> SecondsLeftUpdate;

    public event Action TimerStarted;
    public event Action TimerExpired;
    public event Action TimerStopped;

    public int SecondsForTurn
    {
        get { return _secondsForTurn; }
        set { _secondsForTurn = value; }
    }

    public void StartTimer()
    {
        StopTimer();

        _countDown = StartCoroutine(CountDownSeconds());
    }

    public void StopTimer(bool isSendEvent = false)
    {
        if (_countDown != null)
        {
            StopCoroutine(_countDown);

            if (TimerStopped != null)
                TimerStopped();
        }
    }

    private Coroutine _countDown;

    IEnumerator CountDownSeconds()
    {
        if (TimerStarted != null)
            TimerStarted();

        DateTime startTime = DateTime.UtcNow;

        while (DateTime.UtcNow.Subtract(startTime).TotalSeconds <= _secondsForTurn)
        {
            int counter = _secondsForTurn - (int)DateTime.UtcNow.Subtract(startTime).TotalSeconds;

            if (SecondsLeftUpdate != null)
                SecondsLeftUpdate(counter);

            yield return new WaitForSeconds(1f);
        }

        if (TimerExpired != null)
            TimerExpired();

        _countDown = null;
    }
}
