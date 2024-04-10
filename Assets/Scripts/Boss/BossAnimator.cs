using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimator : MonoBehaviour
{
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<HealthEnemyController>().getCurHealt()<=0)
        {
            GetComponent<BossController>().enabled = false;
            GetComponent<BossCombat>().enabled = false;
        }
    }

    public void SetTriggerAttack(int cb)
    {
        animator.SetTrigger("CB" + cb);
    }
    public void SetSpeed(float speed)
    {
        animator.SetFloat("speed", speed);
    }
    public void SetMagicAttack()
    {
        animator.SetTrigger("MagicAttack");
    }
    public void DesObj()
    {
        Destroy(this.gameObject);
    }
    public void death()
    {
        animator.SetTrigger("Die");
    }
}
