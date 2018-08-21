using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstSelectable : MonoBehaviour {

	private void OnEnable()
	{
		StartCoroutine(SetFirstOption());
	}

	IEnumerator SetFirstOption()
	{
		yield return new WaitForSecondsRealtime(0.05f);
		gameObject.GetComponent<Button>().Select();
		print("Enabled!");
	}
}
