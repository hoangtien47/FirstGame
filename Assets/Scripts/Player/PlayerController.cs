using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public HealthController healthBar;
    public HealthController manaBar;
    public GameObject particleHeal;
    public GameObject loseScene;
    public bool isDead = false;
    [SerializeField]private int maxHealth = 100;
    [SerializeField]private int curHealth;
    [SerializeField] private int maxMana = 100;
    [SerializeField] private int curMana;
    // Start is called before the first frame update
    void Start()
    {
        curHealth = maxHealth;
        curMana = maxMana;
        healthBar.setMaxHealth(maxHealth);
        healthBar.setHealt(curHealth);
        manaBar.setMaxHealth(maxMana);
        manaBar.setHealt(curMana);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (curMana >= 40)
            {
                Instantiate(particleHeal, transform.position, transform.rotation);
                heal();
            }
        }
    }
    public void TakeDamage(int Damgage)
    {
        if (isDead)
        {
            Debug.LogError(Damgage);

            return;
        }
        else
        {
            FindAnyObjectByType<AudioManager>().Play("PlayerHurt");
            curHealth -= Damgage;
            if (curHealth <= 0)
            {
                isDead = true;
                this.gameObject.GetComponent<PlayerAnimator>().SetTriggerDie();
                StartCoroutine(setLose());
            }
            PlayerInfo.health = curHealth;
            healthBar.setHealt(curHealth);
            this.gameObject.GetComponent<PlayerAnimator>().SetTriggerHurt();
        }
    }
    public void increaseMana(int Damgage)
    {
        curMana += Damgage;
        if (curMana > 100)
        {
            curMana = 100;
        }
        PlayerInfo.mana = curMana;
        manaBar.setHealt(curMana);
    }
    public void heal()
    {
        curMana -= 40;
        if(curMana < 0)
        {
            curMana = 0;
        }
        curHealth += 20;
        PlayerInfo.mana = curMana;
        if (curHealth > 100)
        {
            curHealth = 100;
        }
        PlayerInfo.health = curHealth;
        healthBar.setHealt(curHealth);
        manaBar.setHealt(curMana);
    }
    public void setHealth()
    {
        curHealth = PlayerInfo.health;
        curMana = PlayerInfo.mana;
        healthBar.setHealt(curHealth);
        manaBar.setHealt(curMana);
    }
    IEnumerator setLose()
    {       
        this.gameObject.GetComponent<PlayerMovement>().isDead = true;
        FindAnyObjectByType<AudioManager>().Play("PlayerDie");
        this.gameObject.GetComponent<PlayerCombatBehaviour>().isDead = true;
        yield return new WaitForSeconds(1f);
        loseScene.GetComponent<TransionAnimation>().GameOver();
    }
}
