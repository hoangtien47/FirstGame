using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class PlayerData 
{
    public int level;
    public int health;
    public int mana;


    public PlayerData() 
    {
        level = SceneManager.GetActiveScene().buildIndex;
        health = PlayerInfo.health;
        mana = PlayerInfo.mana;

    }

}
