using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedState : IEnemyState
{
    private EnemyDragon enemy;
    private float throwTimer;
    private float throwCooldown = 1;
    private bool canThrow;
    public void Enter(EnemyDragon enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        Throw();

        if(enemy.InMeleeRange)
        {
            enemy.ChangeState(new MeleeState());
        }
        else if(enemy.Target != null)
        {
            enemy.Move();
        }
        else
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

    private void Throw()
    {
        throwTimer += Time.deltaTime;

        if(throwTimer >= throwCooldown)
        {
            canThrow = true;
            throwTimer = 0;
            throwCooldown = 3;
        }

        if(canThrow)
        {
            canThrow = false;
            enemy.myAnimator.SetTrigger("throw");
        }
    }
}
