using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class BossCombat : MonoBehaviour
{
    [SerializeField] public GameObject magicAttackPrepabs;
    [SerializeField] public Transform player;
    Vector3 posMagic = new Vector3 (2, 3, 0);
    BossController controller;
    BossAnimator animator;
    bool atCB;
    bool addForce;
    int CB;
    public Transform hitPoint;
    public float hitRange;
    public GameObject SFace;
    public LayerMask pLayer;
    public int countHit = 0;
    void Start()
    {
        controller = GetComponent<BossController>();
        animator = GetComponent<BossAnimator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(countHit == 3)
        {
            FinishCB();
            SpawnMagicCombo();
        }
    }

    public void StarCB()
    {
        controller.SetInCB(true);
        animator.SetTriggerAttack(CB);
        atCB = false;
        if (CB < 2)
        {
            CB++;
        }
        
    }
    public void SetSface()
    {
        Collider2D hit = Physics2D.OverlapCircle(hitPoint.position, hitRange, pLayer);
        if (hit != null)
        {
            if (countHit < 3)
            {
                countHit++;
                hit.gameObject.GetComponent<PlayerController>().TakeDamage(10);
                SFace.GetComponent<SFaceControll>().ChangeSprite(countHit - 1);
            }
        }
    }

    public void FinishCB()
    {
        controller.SetInCB(false);
        atCB = false;
        CB = 0;
    }
    void ForceOn()
    {
        addForce = true;
    }
    void ForceOff()
    {
        addForce = false;
    }

    public bool GetForce()
    {
        return addForce;
    }
    private void OnDrawGizmosSelected()
    {
        if (hitPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(hitPoint.position, hitRange);
    }
    public void SpawnMagic()
    {
        Instantiate(magicAttackPrepabs, player.position + posMagic, player.rotation);
    }
    private void SpawnMagicCombo()
    {
        if (countHit > 0)
        {
            SFace.GetComponent<SFaceControll>().ChangeSprite(countHit - 1);
            countHit--;
            animator.SetMagicAttack();
        }
        if(countHit == 0)
        {
            SFace.GetComponent<SFaceControll>().Reset();
        }
    }
}
