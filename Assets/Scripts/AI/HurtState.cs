
using System.Collections;
using UnityEngine;
using static Enemy;

public class HurtState : IState
{
    private Enemy enemy;

    public HurtState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    public void EnterState()
    {
        enemy.aiPath.canMove = false;
        enemy.AudioManager.Play(enemy.name + "Hurt");
        enemy.animator.SetInteger("state", (int)MovementState.hurt);
        enemy.StartCoroutine(DestroyAfterDelay(0.6f));
    }

    public void ExitState()
    {
        enemy.aiPath.canMove = true;
    }


    public IState UpdateState()
    {
        return this;
    }
    private IEnumerator DestroyAfterDelay(float delay)
    {
        // Wait for the death animation to finish (you may adjust the time)
        yield return new WaitForSeconds(delay);

        // Destroy the gameObject
        enemy.stateManager.ChangeState(enemy.ChaseState);
    }
}

