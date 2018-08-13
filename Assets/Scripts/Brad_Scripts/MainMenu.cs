using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void PlayGame()
    {
        SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
       SceneManager.LoadScene(3);
    }

    public void Title()
    {
        SceneManager.LoadScene(1);
    }

    public void Credit()
    {
       SceneManager.LoadScene(1);
    }
}
