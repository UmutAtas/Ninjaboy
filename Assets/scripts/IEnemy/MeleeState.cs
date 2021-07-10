using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeState : IEnemyState
{
    private float attackTimer;
    private float attackCooldown = 3f;
    private bool canAttack = true;
    private Enemy enemy;
    public void Execute()
    {
        Attack();
        if (!enemy.InMeleeRange)
        {
            enemy.ChangeState(new RangedState());
        }
    }

    public void Enter(Enemy enemy)
    {
        this.enemy = enemy;
    }

    public void Exit()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        
    }

    private void Attack()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer >= attackCooldown)
        {
            canAttack = true;
            attackTimer = 0;
        }

        if (canAttack)
        {
            canAttack = false;
            enemy.MyAnimator.SetTrigger("attack");
        }
    }
}
