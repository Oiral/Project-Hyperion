using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;

public class StartAutoDialogue : MonoBehaviour {

	[Header("First text node")]
	public string nodeToStart = "";
	[Header("Fade In Card")]
	public GameObject cardToFade;
	public Color startColour;
	public Color endColour;
	[Header("background for text")]
	public GameObject textBackGround;
	public Color textCardStartColour;
	public Color textCardEndColour;

	public float lengthOfFade;
	// Use this for initialization
	void Start () {
		StartCoroutine(RunStartScene());
	}
	IEnumerator RunStartScene()
	{
		yield return StartCoroutine(FadeIn());
		if (nodeToStart != "")
		{

			FindObjectOfType<DialogueRunner>().StartDialogue(nodeToStart);
		}
	}

	IEnumerator FadeIn()
	{
		WaitForEndOfFrame waitStep = new WaitForEndOfFrame();
		if (cardToFade != null)
		{
			textBackGround.SetActive(true);
			for (float t = 0; t < lengthOfFade; t += Time.deltaTime)
			{
				cardToFade.GetComponent<Image>().color = Color.Lerp(startColour, endColour, t/lengthOfFade);
				textBackGround.GetComponent<Image>().color = Color.Lerp(textCardStartColour, textCardEndColour, t/lengthOfFade);
				yield return waitStep;
			}
			cardToFade.GetComponent<Image>().color = endColour;
			cardToFade.SetActive(false);
		}
		else
		{
			yield return waitStep;
		}
		
	}
}
