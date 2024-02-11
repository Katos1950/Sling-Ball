using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    [SerializeField] Canvas winCanvas;
    [SerializeField] Canvas loseCanvas;
    [SerializeField] Canvas pauseRetryCanvas;
    [SerializeField] bool wantPauseRetryCanvas;
    [SerializeField] TextMeshProUGUI PlayerLeftText;
    [SerializeField] Canvas helpCanvas;
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] VideoClip[] videoClips;

    [HideInInspector]public GameObject players;
    public int playerCount;
    int starttingEnemies;
    public int enemies = 0;
    bool isPaused = false;
    int currentSceneIndex;
    LevelCounter levelCounter;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        levelCounter = FindObjectOfType<LevelCounter>();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        winCanvas.enabled = false;
        loseCanvas.enabled = false;
        helpCanvas.enabled = false;

        if(!wantPauseRetryCanvas)
        {
            pauseRetryCanvas.enabled = false;
        }

        if(wantPauseRetryCanvas)
        {
            players = GameObject.Find("Players");
            playerCount = players.transform.childCount;
            PlayerLeftText.text = playerCount.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(enemies == 0 && starttingEnemies !=0)
        {
            StartCoroutine("WinSequence");
        }
    }

    private IEnumerator WinSequence()
    {
        yield return new WaitForSeconds(2f);
        if(loseCanvas.enabled == false)
        {
            winCanvas.enabled = true;
            levelCounter.DetermineMaxLevel(currentSceneIndex + 1);
        }
    }

    public IEnumerator LoseSequence()
    {
        yield return new WaitForSeconds(10f);
        if(winCanvas.enabled == false)
        {
            loseCanvas.enabled = true;
        }
    }

    public void AddEnemy()
    {
        starttingEnemies++;
        enemies++;
    }

    public void SubtractEnemy()
    {
        enemies--;
    }

    public void ChangePlayersLeftText()
    {
        playerCount--;
        PlayerLeftText.text = playerCount.ToString();
    }

    public void OpenHelpMenu()
    {
        helpCanvas.enabled = true;
    }

    public void HideHelpMenu()
    {
        helpCanvas.enabled = false;
    }

    public void PlayVideoClip(int videoNo)
    {
        videoPlayer.clip = videoClips[videoNo];
        videoPlayer.Play();
    }

    //Scene Management and buttons
    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
        isPaused = false;
    }
    
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Retry()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        isPaused = false;
    }

    public void PauseUnpause(GameObject pauseMenu)
    {
        if(isPaused)
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            isPaused = false;
        }
        else
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            isPaused = true;
        }
    }

    public void ButtonLevelLoad(GameObject levelName)
    {
        SceneManager.LoadScene(levelName.name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
