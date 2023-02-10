using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private static readonly int Idle = Animator.StringToHash("Idle");
    private static readonly int Walk = Animator.StringToHash("Walk");
    private static readonly int Run = Animator.StringToHash("Run");
    private static readonly int JumpRebound = Animator.StringToHash("JumpRebound");
    
    public void PlayIdle()
    {
        animator.SetBool(Idle,true);
        animator.SetBool(Walk,false);
        animator.SetBool(Run,false);
    }

    public void PlayWalk()
    {
        animator.SetBool(Idle,false);
        animator.SetBool(Walk,true);
        animator.SetBool(Run,false);
    }

    public void PlayRun()
    {
        animator.SetBool(Idle,false);
        animator.SetBool(Walk,false);
        animator.SetBool(Run,true);
    }
    
    public void PlayJump()
    {
        animator.SetTrigger(JumpRebound);
    }
}
