using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    private const string MOVING = "Moving";
    private const string DEATH = "Death";
    private const string SWORD_ATTACK = "SwordAttack";
    private const string AREA_ATTACK = "AreaAttack";

    private Player player;
    private Animator animator;

    private void Awake()
    {
        player = GetComponent<Player>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleMoveAnimation();
    }

    private void HandleMoveAnimation()
    {
        if (player.GetMoveDirection() != Vector3.zero)
        {
            animator.SetBool(MOVING, true);
        }
        else
        {
            animator.SetBool(MOVING, false);    
        }
    }

    public void TriggerDeathAnimation()
    {
        animator.SetTrigger(DEATH);
    }

    public void TriggerSwordAttackAnimation()
    {
        animator.SetTrigger(SWORD_ATTACK);
    }

    public void TriggerAreaAttackAnimation()
    {
        animator.SetTrigger(AREA_ATTACK);
    }
}
