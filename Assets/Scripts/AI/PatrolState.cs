using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;
using static UnityEngine.EventSystems.EventTrigger;

public class PatrolState : IState
{
    private Enemy enemy;
    public PatrolState( Enemy enemy)
    {
        this.enemy = enemy;
    }
    public void EnterState()
    {
        enemy.AudioManager.Play(enemy.name + "Walk");

        enemy.animator.SetInteger("state", (int)MovementState.running);

        // Enter Patrol state behavior
        Debug.Log("enter patrol");

    }

    public void ExitState()
    {
        // Exit Patrol state behavior
    }



    public IState UpdateState()
    {

        DoPatrol(enemy);
        // Patrol state behavior
        if (PlayerInSight(enemy))
        {
            return enemy.ChaseState;
        }
        

        return this;
    }

    private void DoPatrol(Enemy enemy)
    {
        enemy.FlipSprite(enemy.wayPointFollower.GetNextWaypoint().transform);

        enemy.wayPointFollower.Patrol();
        
    }

    private bool PlayerInSight(Enemy enemy)
    {
        Vector2 enemyPosition = enemy.transform.position;
        Vector2 playerPosition = enemy.player.transform.position; // Assuming you have a reference to the player

        // Calculate the direction vector from the enemy to the player
        Vector2 direction = playerPosition - enemyPosition;

        // Perform a raycast from the enemy to the player
        RaycastHit2D hit = Physics2D.Raycast(enemyPosition, direction, enemy.sightDistance, enemy.sightLayerMask);

        // Check if the ray hits the player
        if (hit.collider != null && hit.collider.CompareTag("Player"))
        {
            return true;
        }

        return false;
    }
}

