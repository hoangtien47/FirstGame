
using System.Collections;
using UnityEngine;
using static Enemy;

public class SpawnState : IState
{
    private Enemy enemy;

    public SpawnState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    public void EnterState()
    {
        Debug.Log("Spawn");
        //float randomFloat = Random.value;
        enemy.aiPath.canMove = false;
        enemy.animator.SetInteger("state", (int)MovementState.spawn);
        enemy.AudioManager.Play(enemy.name + "Spawn");
        // Enter Attack state behavior
        enemy.StartCoroutine(CheckForHitsAfterDelay(1.5f));
    }

    public void ExitState()
    {
        enemy.aiPath.canMove = true;
    }


    public IState UpdateState()
    {
        return this;
    }


    private IEnumerator CheckForHitsAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        // After 1 second, check for hits        
        enemy.stateManager.ChangeState(enemy.ChaseState);

    }
}

