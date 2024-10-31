using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{

    private const string CHASE = "Chase";
    private const string ATTACK = "Attack";

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayIdleAnimation()
    {
        animator.SetBool(CHASE, false);
    }

    public void PlayChasingAnimation()
    {
        animator.SetBool(CHASE, true);
    }

    public void PlayAttackAnimation()
    {
        animator.SetTrigger(ATTACK);
    }
}
