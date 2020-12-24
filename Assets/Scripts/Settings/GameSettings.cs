
using UnityEngine;

public class GameSettings : ScriptableObject
{
    [SerializeField] private int _afterRoundWaiting = 6;
    [SerializeField] private int _rematchWaiting = 9;
    [SerializeField] private float _attackTime = .7f;
    [SerializeField] private int _submitExpBot = 6;
    [SerializeField] private int _submitExpFriendFight = 6;

    public int AfterRoundWaiting { get { return _afterRoundWaiting; } }
    public int RematchWaiting { get { return _rematchWaiting; } }
    public float AttackTime { get { return _attackTime; } }
    public int SubmitExpBot { get { return _submitExpBot; } }
    public int SubmitExpFriendFight { get { return _submitExpFriendFight; } }
}