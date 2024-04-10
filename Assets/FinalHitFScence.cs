using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalHitFScence : MonoBehaviour
{
    public GameObject Final;
    public HealthEnemyController health;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(health.getCurHealt() <= 600)
        {
            Final.active = true;
        }
    }
}
