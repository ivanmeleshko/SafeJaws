using System;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public event Action Over;

    public void Run()
    {
        animator.SetTrigger("run");
        Invoke("OnEventExecute", 3f);
    }

    public void OnOver()
    {
        //if (Over != null) Over();
    }

    public void OnEventExecute()
    {
        if (Over != null) Over();
    }
}
