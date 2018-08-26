using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManagerSurrogate : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
		//FindFile();
	}
}
