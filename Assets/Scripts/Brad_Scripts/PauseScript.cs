using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {

    public void UnPauseGame()
    {
        SceneManager.LoadScene(GameManager.previousSceneIndex);
    }

    public void Return()
    {
        SceneManager.LoadScene(4);
    }
}
