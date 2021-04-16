using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject deadPanel;
    public GameObject optionPanel;
    public playerManager checkLife;

    public string sceneGame;
    public string mainMenuScene;
    public soudScript sound;

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
        AudioSource.PlayClipAtPoint(sound.botton, transform.position);
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void RestartGame()
    {
        AudioSource.PlayClipAtPoint(sound.botton, transform.position);
        SceneManager.LoadScene(sceneGame);
    }

    public void MainMenu()
    {
        AudioSource.PlayClipAtPoint(sound.botton, transform.position);
        SceneManager.LoadScene(mainMenuScene);
    }

    public void CloseGame()
    {
        AudioSource.PlayClipAtPoint(sound.botton, transform.position);
        Application.Quit();
    }

    public void OpenOptionPanel()
    {
        AudioSource.PlayClipAtPoint(sound.botton, transform.position);
        pausePanel.SetActive(false);
        optionPanel.SetActive(true);
    }

    public void CloseOptionPanel()
    {
        AudioSource.PlayClipAtPoint(sound.botton, transform.position);
        optionPanel.SetActive(false);
        pausePanel.SetActive(true);
    }
}
