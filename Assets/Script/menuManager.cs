using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{
    public string nameGameScene;
    public GameObject optionPanel;
    public GameObject menuPanel;
    public AudioClip soundBotton;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        AudioSource.PlayClipAtPoint(soundBotton, transform.position);
        SceneManager.LoadScene(nameGameScene);
    }

    public void OpenOptionPanel()
    {
        AudioSource.PlayClipAtPoint(soundBotton, transform.position);
        menuPanel.SetActive(false);
        optionPanel.SetActive(true);
    }

    public void CloseOptionPanel()
    {
        AudioSource.PlayClipAtPoint(soundBotton, transform.position);
        menuPanel.SetActive(true);
        optionPanel.SetActive(false);
    }

    public void ExitGame()
    {
        AudioSource.PlayClipAtPoint(soundBotton, transform.position);
        Application.Quit();
    }
}
