using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineController : MonoBehaviour
{
    public GameObject p;
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(10f);
        canvas.SetActive(true);
        p.SetActive(true);
        
        this.gameObject.SetActive(false);
    }
}
