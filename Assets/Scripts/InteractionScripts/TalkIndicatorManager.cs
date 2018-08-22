using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class TalkIndicatorManager : MonoBehaviour {

	public GameObject talkIndicator;

	private void Start()
	{
		GameManager.instance.talkIndicator = gameObject;
		StartCoroutine(TurnOffSelf());
	}
	
	IEnumerator TurnOffSelf()
	{
		yield return new WaitForEndOfFrame();
		gameObject.SetActive(false);
	}
	// Update is called once per frame
	void Update () {
		//Sets the talk button to be off while the dialogue is being run and on when the dialogue is finished
		talkIndicator.SetActive(!FindObjectOfType<DialogueRunner>().isDialogueRunning);
	}
}
