
using System.Collections;
using UnityEngine;
using static Enemy;

public class DieState : IState
{
    private Enemy enemy;

    public DieState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    public void EnterState()
    {
        enemy.aiPath.canMove = false;
        enemy.AudioManager.Play(enemy.name + "Die");
        enemy.animator.SetInteger("state", (int)MovementState.death);
        enemy.StartCoroutine(DestroyAfterDelay(1f));
    }

    public void ExitState()
    {

    }


    public IState UpdateState()
    {
        return this;
    }
    private IEnumerator DestroyAfterDelay(float delay)
    {


        // Wait for the death animation to finish (you may adjust the time)
        yield return new WaitForSeconds(delay);
        enemy.spawnPoint();
        // Destroy the gameObject
        UnityEngine.Object.Destroy(enemy.destroyGameObject);
    }
}

