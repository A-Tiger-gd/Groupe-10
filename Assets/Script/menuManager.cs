using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{
    public string nameGameScene;
    public GameObject optionPanel;
    public GameObject menuPanel;

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
        SceneManager.LoadScene(nameGameScene);
    }

    public void OpenOptionPanel()
    {
        menuPanel.SetActive(false);
        optionPanel.SetActive(true);
    }

    public void CloseOptionPanel()
    {
        menuPanel.SetActive(true);
        optionPanel.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
