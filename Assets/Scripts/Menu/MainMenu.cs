using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void PlayGame()
    {
        SceneFlow.RunScene(SceneList.MainScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

	//Please Rename something like ReturnToMainMenu etc.
    public void Credits()
    {
		SceneFlow.RunScene(SceneList.Credits);
    }
    
	//Rename Credits
    public void ReturnToMainMenu()
    {
		SceneFlow.RunScene(SceneList.MainMenu);
	}
}
