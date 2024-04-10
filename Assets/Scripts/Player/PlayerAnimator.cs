using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class PlayerAnimator : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Speed", Mathf.Abs(playerMovement.horizontal));
        animator.SetBool("OnGround", playerMovement.IsGrounded());
        animator.SetFloat("VelocityY", playerMovement.rb.velocity.y);
        SetBoolWall(playerMovement.StopWallonGrounnd());
    }

    public void SetDashAttack()
    {
        animator.SetTrigger("DashAttack");
    }
    public void SetDashing()
    {
        animator.SetTrigger("Dashing");
    }

    public void setAttack(int i)
    {
        animator.SetTrigger("CB" + i);

    }

    public void resetTriggerAttack(int i)
    {
        animator.ResetTrigger("CB" + i);
    }

    public void SetBoolWall(bool isWall)
    {
        animator.SetBool("OnWall", isWall);
    }
    public void SetTriggerHurt()
    {
        animator.SetTrigger("Hurt");

    }
    public void SetTriggerDie()
    {
        animator.SetTrigger("Die");
    }
    public void PlayRun()
    {
        FindAnyObjectByType<AudioManager>().Play("PlayerRun");
    }
}
