using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

public class ChaseState : IState
{
    private Enemy enemy;
    public ChaseState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    public void EnterState()
    {
        Debug.Log("Chase");

        enemy.animator.SetInteger("state", (int)MovementState.running);

        // Enter Chase state behavior
    }

    public void ExitState()
    {
        // Exit Chase state behavior
    }


    public IState UpdateState()
    {
        DoChase(enemy);
        // Chase state behavior
        if (PlayerLost(enemy))
        {
            return enemy.PatrolState;
        }
        if (PlayerInAttackRange(enemy))
        {
            return enemy.AttackState;
        }
        return this;
    }

    private void DoChase(Enemy enemy)
    {
        enemy.FlipSprite(enemy.player.transform);
        if (enemy.player != null)
        {
            //Vector3 directionToPlayer = (enemy.player.transform.position - enemy.transform.position).normalized;

            //// Define the movement speed
            //float moveSpeed = 5f; // Adjust this value as needed

            //// Move the enemy towards the player
            //enemy.transform.position += directionToPlayer * moveSpeed * Time.deltaTime;
            enemy.aiPath.maxAcceleration = 20;
            enemy.aiPath.maxSpeed = 5;
            enemy.aiPath.destination = enemy.player.transform.position;
        }
    }
    private bool PlayerLost(Enemy enemy)
    {
        // Implement your player loss condition here
        // For example, check if the player is too far away
        if (enemy.player != null)
        {
            Vector2 playerPosition = enemy.player.transform.position;
            float distanceToPlayer = Vector2.Distance(enemy.transform.position, playerPosition);

            // If the player is too far away, return true
            return distanceToPlayer > enemy.sightDistance;
        }

        // If the player is not found or is within the chase distance, return false
        return false;
    }
    private bool PlayerInAttackRange(Enemy enemy)
    {
        // Implement your player loss condition here
        // For example, check if the player is too far away
        if (enemy.player != null)
        {
            Vector2 playerPosition = enemy.player.transform.position;
            float distanceToPlayer = Vector2.Distance(enemy.transform.position, playerPosition);

            // If the player is too far away, return true
            return distanceToPlayer < enemy.attackDistance;
        }

        // If the player is not found or is within the chase distance, return false
        return false;
    }
}

