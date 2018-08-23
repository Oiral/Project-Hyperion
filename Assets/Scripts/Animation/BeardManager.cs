using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeardManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(CheckBeard());
	}
	//If Gauntlets are complete, set beard to active!
	IEnumerator CheckBeard()
	{
		yield return new WaitForEndOfFrame();
		gameObject.SetActive(GameManager.instance.CheckGauntlets());
	}
}
