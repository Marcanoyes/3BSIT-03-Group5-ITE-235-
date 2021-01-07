﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeState : IEnemyState
{
    private EnemyDragon enemy;
    private float attackTimer;
    private float attackCooldown = 1;
    private bool canAttack = true;
    public void Enter(EnemyDragon enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        EnemyMeleeAttack();
        if(enemy.InThrowRange && !enemy.InMeleeRange)
        {
            enemy.ChangeState(new RangedState());
        }
        else if (enemy.Target == null)
        {
            enemy.ChangeState(new IdleState());
        }
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {

    }

    private void EnemyMeleeAttack()
    {
        attackTimer += Time.deltaTime;

        if(attackTimer >= attackCooldown)
        {
            canAttack = true;
            attackTimer = 0;
            attackCooldown = 1.3f;
        }

        if(canAttack)
        {
            canAttack = false;
            enemy.myAnimator.SetTrigger("attack");
        }
    }
}
