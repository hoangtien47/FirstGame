using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatBehaviour : MonoBehaviour
{
    public bool isDead = false;
    public Transform hitPoint;
    public float hitRange = 1f;
    private bool Addforce;
    public LayerMask objectLayer;
    PlayerMovement playerMovement;
    public LayerMask enemyLayer;
    public int AttackDamage = 60;
    public float combatRate = 2f;
    public int combo;
    public bool atCB;

    public bool atDash;

    [SerializeField] private Vector2 Force;


    // Update is called once per frame
    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }
    void Update()
    {
        if(isDead) return;
        if (!playerMovement.isJumping && !playerMovement.isFalling)
        {
            if (playerMovement.isDashing)
            {
                if (Input.GetKeyDown(KeyCode.C) && !atDash)
                {
                    atDash = !atDash;
                    DashAttack();
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.C) && !atCB)
                {
                    atCB = !atCB;
                    Attack();
                }
            }
        }
        if (playerMovement.isJumping || playerMovement.isFalling)
        {
            combo = 0;
        }
    }

    public void Attack()
    {
        gameObject.GetComponent<PlayerAnimator>().setAttack(combo);
        gameObject.GetComponent<PlayerMovement>().SetInCB(true);
        HitEnemySuccess();
    }
    private void DashAttack()
    {
        gameObject.GetComponent<PlayerAnimator>().SetDashAttack();
        gameObject.GetComponent<PlayerMovement>().SetInDash(true);
        HitEnemySuccess();
    }
    private void OnDrawGizmosSelected()
    {
        if (hitPoint == null)
        {
            return;
        }

        Gizmos.DrawWireSphere(hitPoint.position, hitRange);
    }

    public void Start_CB()
    {
        atCB = false;
        if (combo < 3)
        {
            FindAnyObjectByType<AudioManager>().Play("PlayerAttack" + combo);
            combo++;
        }
    }

    public void Finish_CB()
    {
        gameObject.GetComponent<PlayerAnimator>().resetTriggerAttack(combo);
        gameObject.GetComponent<PlayerMovement>().SetInCB(false);
        gameObject.GetComponent<PlayerMovement>().SetInDash(false);
        atCB = false;
        combo = 0;
    }

    public void Start_Dash()
    {
        gameObject.GetComponent<PlayerMovement>().SetInDash(true);
        atDash = false;
    }

    public void Finish_Dash()
    {
        gameObject.GetComponent<PlayerMovement>().SetInDash(false);
        gameObject.GetComponent<PlayerMovement>().SetInCB(false);
        atDash = false;
    }

    void ForceOn()
    {
        Addforce = true;
    }
    void ForceOff()
    {
        Addforce = false;
    }

    public bool GetForce()
    {
        return Addforce;
    }
    private void HitEnemySuccess()
    {
        Collider2D hit = Physics2D.OverlapCircle(hitPoint.position, hitRange, enemyLayer);
        if (hit != null)
        {
            if (hit.gameObject.GetComponent<HealthEnemyController>() == null)
            {
                hit.gameObject.GetComponent<Enemy>().stateManager.ChangeState(hit.gameObject.GetComponent<Enemy>().HurtState);
                hit.gameObject.GetComponent<HealthSmallEnemyController>().TakeDamage(20);
            }
            else
                hit.gameObject.GetComponent<HealthEnemyController>().TakeDamage(20);

        }
    }
}
