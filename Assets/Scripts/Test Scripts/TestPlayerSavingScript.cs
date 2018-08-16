using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerSavingScript : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.N))
        {
            SceneFlow.RunScene(SceneList.MainScene);
        }
	}
}
