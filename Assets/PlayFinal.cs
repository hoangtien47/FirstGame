using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFinal : MonoBehaviour
{
    public GameObject image;
    public GameObject p;
    public GameObject w,k;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartEnd());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator StartEnd()
    {
        p.GetComponent<PlayerController>().enabled = false;
        p.GetComponent<PlayerCombatBehaviour>().enabled = false;
        p.GetComponent<PlayerAnimator>().enabled = false;
        p.GetComponent<PlayerMovement>().enabled = false;
        w.active = true;
        k.active = true;
        yield return new WaitForSeconds(5.4f);
        p.GetComponent <Animator>().enabled=false;
        image.active = true;
    }
}
