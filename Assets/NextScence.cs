using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScence : MonoBehaviour
{
    public float time;
    public BoxCollider2D box;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nextScenceStart(time));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator nextScenceStart(float timeLoad)
    {
        yield return new WaitForSeconds(timeLoad);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Player"))
            StartCoroutine(nextScenceStart(3f));
    }
}
