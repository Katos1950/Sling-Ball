using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelEnabler : MonoBehaviour
{
    void Start()
    {
        LevelCounter levelCounter = FindObjectOfType<LevelCounter>();
        Component[] buttons = GetComponentsInChildren<Button>();
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }

        for(int i = 0;i<= levelCounter.maxLevel-2;i++)
        {
            //i = Mathf.Clamp(i, 0, buttons.Length - 1);
            try
            {
                transform.GetChild(i).gameObject.GetComponent<Button>().interactable = true;
            }
            catch
            {
                //do nothing
            }
        }
    }
}
