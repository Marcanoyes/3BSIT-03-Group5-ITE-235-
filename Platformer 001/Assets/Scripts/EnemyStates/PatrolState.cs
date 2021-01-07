using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IEnemyState
{
    private EnemyDragon enemy;
    private float patrolTimer;
    private float patrolDuration = 7;
    public void Enter(EnemyDragon enemy)
    {
        this.enemy = enemy;
    }

    public void Execute()
    {
        Patrol();
        enemy.Move();

        if(enemy.Target != null && enemy.InThrowRange)
        {
            enemy.ChangeState(new RangedState());
        }
    }

    public void Exit()
    {

    }

    public void OnTriggerEnter(Collider2D other)
    {
        // if(other.tag == "Edge")
        // {
        //     enemy.ChangeDirection();
        // }

        if(other.tag == "PlayerProjectile" || other.tag == "Sword" )
        {
            enemy.Target= Player.Instance.gameObject;
        }
    }

    private void Patrol()
    {
        patrolTimer += Time.deltaTime;

        if(patrolTimer >= patrolDuration)
        {
            enemy.ChangeState(new IdleState());
        }
    }
}
