using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemyController : MonoBehaviour
{
    [SerializeField] public HealthController healthBar; 
    [SerializeField] private int maxHealth;
    [SerializeField] private int curHealth;
    public BossAnimator bossAnimator;
    public GameObject end;
    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
        healthBar.setHealt(curHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (curHealth <= 0)
        {
            GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject gameObject in gameObjects)
            {
                Destroy(gameObject);
            }
            bossAnimator.death();
            StartCoroutine(endScene());
        }
    }
    public void TakeDamage(int Damgage)
    {
        curHealth -= Damgage;
        healthBar.setHealt(curHealth);
    }
    public int getCurHealt()
    {
        return this.curHealth;
    }
    IEnumerator endScene()
    {
        yield return new WaitForSeconds(0.6f);
        end.active = true;
    }
}
