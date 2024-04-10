using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransionAnimation : MonoBehaviour
{
    public GameObject Img;
    public GameObject Scene;
    // Start is called before the first frame update
    void Start()
    {
        SaveSystem.SavePlayer();
        StartCoroutine(GameStart());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(1.5f);
    }
    public void GameOver()
    {
        StartCoroutine(ImgOver());
    }
    IEnumerator ImgOver()
    {
        yield return new WaitForSeconds(1f);
        Scene.active = true;
        Img.GetComponent<Animator>().SetTrigger("Lose");
    }
    public void ResetScene()
    {
        Debug.LogError("Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
