using System;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;

public class YarnFunctionsManager : MonoBehaviour {

	[System.Serializable]
	public struct BustInfo
	{
		public string name;
		public Sprite bust;
	}

	public Image otherBust;
	public Image nateBust;

	public BustInfo[] busts;

	///TODO: FIX THIS 
	[YarnCommand("startbattle")]
	public void StartBattle(string character)
	{
		GameManager.instance.SetEnemy(character);
		GameManager.instance.StartBattle();
	}

	[YarnCommand("openshop")]
	public void OpenShop()
	{
		SceneFlow.RunScene(SceneList.Shop);
	}

	[YarnCommand("setbust")]
	public void UseBust(string bustName)
	{
		nateBust.color = Color.white;

		Sprite b = null;
		foreach (var info in busts)
		{
			if (info.name == bustName)
			{
				b = info.bust;
				break;
			}
		}
		if (b == null)
		{
			Debug.LogErrorFormat("Can't find sprite named {0}!", bustName);
			return;
		}

		otherBust.sprite = b;
		otherBust.color = Color.white;
	}

	[YarnCommand("resetbusts")]
	public void ResetBust()
	{
		nateBust.color = new Color(0, 0, 0, 0);
		otherBust.color = new Color(0, 0, 0, 0);


	}

	[YarnCommand("resetgauntlet")]
	public void ResetGauntletRun()
	{
		GameManager.instance.playerHealth = 15;
	}
}
