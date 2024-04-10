using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastCutScence : MonoBehaviour
{
    public GameObject start;
    public GameObject boss;
    public GameObject health;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Enemy") == null)
        {
            start.active = true;
            StartCoroutine(startBossFight());
        }
    }
    IEnumerator startBossFight()
    {
        yield return new WaitForSeconds(6f);
        health.active = true;
        boss.active = true;
        Destroy(start);
        Destroy(this.gameObject);
    }
}
