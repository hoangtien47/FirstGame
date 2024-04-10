using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Enemy;

public class HealthSmallEnemyController : MonoBehaviour
{
    [SerializeField] public HealthController healthBar;
    [SerializeField] private int maxHealth;
    [SerializeField] private int curHealth;
    public Enemy enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        StartCoroutine(waitForCanvas());
    }

    // Update is called once per frame
    void Update()
    {
        if(curHealth <=0)
        {
            enemy.stateManager.ChangeState(enemy.DieState);
        }

    }


    public void TakeDamage(int Damgage)
    {
        curHealth -= Damgage;
        healthBar.setHealt(curHealth);
    }
    IEnumerator waitForCanvas()
    {
        curHealth = maxHealth;
        yield return new WaitForSeconds(0.5f);
        healthBar.setMaxHealth(maxHealth);
        healthBar.setHealt(curHealth);
    }
}
