using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenceSecond : MonoBehaviour
{
    public GameObject timeLine;
    public GameObject player;
    public GameObject canvas;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StarGame());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator StarGame()
    {
        yield return new WaitForSeconds(75f);
        timeLine.SetActive(false);
        canvas.SetActive(true);
        player.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<PlayerController>().setHealth();
    }
}
