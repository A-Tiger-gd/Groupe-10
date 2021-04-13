using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject deadPanel;
    public playerManager checkLife;

    public string sceneGame;
    public string mainMenuScene;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Time.timeScale == 0)
        {
            if (checkLife.life == 0)
                deadPanel.SetActive(true);
        }*/

        if(Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
    }

    public void PlayOnPause()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void RestartGame()
    {    
        SceneManager.LoadScene(sceneGame);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(mainMenuScene);
    }

    public void CloseGame()
    {
        Application.Quit();
    }
}
