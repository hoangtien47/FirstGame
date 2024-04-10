using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public static bool GameIsPause;
    public GameObject PauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(!GameIsPause)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }
    public void PauseGame()
    {
        PauseMenu.active = true;
        Time.timeScale = 0f;
        GameIsPause = true;
    }
    public void ResumeGame()
    {
        PauseMenu.active = false;
        Time.timeScale = 1f;
        GameIsPause = false;
    }
    public void SaveGame()
    {
        SaveSystem.SavePlayer();
        ResumeGame();
        SceneManager.LoadScene(0);
    }
}
