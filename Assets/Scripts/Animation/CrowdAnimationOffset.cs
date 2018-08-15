using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdAnimationOffset : MonoBehaviour {
	public static float timeDelay = 0.05f;
	// Use this for initialization
	void Start () {

		GetComponent<Animator>().Play("Normal_Idea", -1, timeDelay);
		timeDelay += 0.1f + Random.Range(0.1f, 0.2f);
	}
}
