using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstSelectable : MonoBehaviour {

	private void OnEnable()
	{
		gameObject.GetComponent<Button>().Select();
	}
}
