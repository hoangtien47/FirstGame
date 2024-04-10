using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(End());
    }

    // Update is called once per frame
    IEnumerator End()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(0);
    }
}
