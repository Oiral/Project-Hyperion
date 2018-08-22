using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FirstSelectable : MonoBehaviour {

	private void OnEnable()
	{
		float t = 0.05f;
		if (SceneManager.GetActiveScene().name == SceneFlow.GetSceneName(SceneList.Credits))
		{
			t = 1.5f;
		}
		StartCoroutine(SetFirstOption(t));
	}

	IEnumerator SetFirstOption(float t)
	{
		yield return new WaitForSecondsRealtime(t);
		gameObject.GetComponent<Button>().Select();
		//print("Enabled!");
	}
}
