using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCounter:MonoBehaviour
{
    public int maxLevel = 2;
    public int playerPrefMaxLevel;
    private void Start()
    {


        if (FindObjectsOfType<LevelCounter>().Length > 1)
        {
            Destroy(gameObject);
        }
        else;
        {
            DontDestroyOnLoad(gameObject);
        }


        playerPrefMaxLevel = PlayerPrefs.GetInt("playerPrefMaxLevel");
        if(playerPrefMaxLevel == 0)
        {
            playerPrefMaxLevel = maxLevel;
            PlayerPrefs.SetInt("playerPrefMaxLevel", playerPrefMaxLevel);
        }
        if (maxLevel < playerPrefMaxLevel)
        {
            maxLevel = playerPrefMaxLevel;
        }
    }
    public void DetermineMaxLevel(int currentLevel)
    {
        if(currentLevel > maxLevel)
        {
            maxLevel = currentLevel;
        }
        if(maxLevel >= playerPrefMaxLevel)
        {
            playerPrefMaxLevel = maxLevel;
            PlayerPrefs.SetInt("playerPrefMaxLevel", playerPrefMaxLevel);
        }
    }
}
