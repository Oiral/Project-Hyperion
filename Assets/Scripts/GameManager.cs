using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public static int previousSceneIndex;

	// Use this for initialization
	void Awake () {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        { Destroy(gameObject); }
        DontDestroyOnLoad(gameObject);
        previousSceneIndex = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
