using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartScence : MonoBehaviour
{
    public GameObject timeLine2;
    public GameObject field;
    public GameObject e1;
    public GameObject e2;
    public GameObject e3;
    public GameObject e4;

    public BoxCollider2D box;
    // Start is called before the first frame update
    void Start()
    {
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name.Equals("Player"))
        {
            timeLine2.active = true;
            field.active = true;
            StartCoroutine(nextPlay());
        }
    }
    IEnumerator nextPlay()
    {
        yield return new WaitForSeconds(12f);
        timeLine2.active = false;
        e1.active = true;
        e2.active = true;
        e3.active = true;
        e4.active = true;
        Destroy(this.gameObject);
    }
}
