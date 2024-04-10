using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    private Slider healthSlider;
    // Start is called before the first frame update
    void Start()
    {
        healthSlider = GetComponent<Slider>();
    }

    public void setMaxHealth(int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
    }
    public void setHealt(int health)
    {
        healthSlider.value = health;
    }
}
