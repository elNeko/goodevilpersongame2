using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string startingScene;

    public void StartGame()
    {
            SceneManager.LoadScene(startingScene, LoadSceneMode.Single);
    }

    public void OpenMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
    }

    public void OpenEndscreen()
    {
        SceneManager.LoadScene("Endscreen", LoadSceneMode.Single);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
