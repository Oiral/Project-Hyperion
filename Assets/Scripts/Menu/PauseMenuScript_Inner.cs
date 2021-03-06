﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenuScript_Inner : MonoBehaviour {

    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
	[Header("Event System Linked to this object")]
	public GameObject eventSystem;

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
			}

            else
            {
                Pause();
            }
        }
	}

    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
		SceneFlow.RunScene(SceneList.MainMenu);
    }

    public void QuitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }


}
