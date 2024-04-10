using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Text scene;

    public GameObject saveCanvas;
    // Start is called before the first frame update
    public void StartScence()
    {
        StartCoroutine(StartGame());
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadScence() 
    {
        StartCoroutine(Load());
    }

    IEnumerator Load()
    {
        PlayerData playerData = SaveSystem.LoadPlayer();

        if (playerData == null)
        {
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            PlayerInfo.health = playerData.health;
            PlayerInfo.mana = playerData.mana;
            yield return new WaitForSeconds(0.5f);
            SceneManager.LoadScene(playerData.level);
        }
    }

    public void Start()
    {
        PlayerData playerData = SaveSystem.LoadPlayer();
        if (playerData != null )
        {
            scene.text = "Level: "+playerData.level.ToString();
        }
        else
        {
            scene.text = "New Game";

        }
    }

    public void OpenSaveCanvas()
    {
        saveCanvas.active = true;
    }
}
