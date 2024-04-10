
using System.Collections;
using UnityEngine;
using static Enemy;

public class AttackState : IState
{
    private Enemy enemy;

    public AttackState(Enemy enemy)
    {
        this.enemy = enemy;
    }
    public void EnterState()
    {
        Debug.Log("Attack");
        //float randomFloat = Random.value;
        enemy.aiPath.canMove = false;
        enemy.animator.SetInteger("state", (int)MovementState.attack);
        Debug.Log(enemy.name + "Attack");
        enemy.AudioManager.Play(enemy.name + "Attack");
        // Enter Attack state behavior
        enemy.StartCoroutine(CheckForHitsAfterDelay(0.7f));
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
        Collider2D hit = Physics2D.OverlapCircle(enemy.hitPoint.position, enemy.attackDistance, enemy.sightLayerMask);
        if (hit != null)
        {
            hit.gameObject.GetComponent<PlayerController>().TakeDamage(10);
        }
        enemy.stateManager.ChangeState(enemy.ChaseState);

    }
}

