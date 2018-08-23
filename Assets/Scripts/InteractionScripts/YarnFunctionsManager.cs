using System.Collections;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;
using UnityEngine.Analytics;

public class YarnFunctionsManager : MonoBehaviour {

	public GameObject beard;

	[System.Serializable]
	public struct BustInfo
	{
		public string name;
		public string displayName;
		public Sprite bust;
	}

	public Image otherBust;
	public Image nateBust;

	public BustInfo[] busts;

    public AnalyticsEventTracker beatGauntletTracker;
    public AnalyticsEventTracker repeatBeatGauntletTracker;
    public string currentGauntletName = "Unassigned gauntlet";

	#region YarnCommands
	///TODO: FIX THIS 
	[YarnCommand("startbattle")]
	public void StartBattle(string character)
	{
		GameManager.instance.SetEnemy(character, this);
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
        currentGauntletName = "Unassigned gauntlet";
		GameManager.instance.playerHealth = 15;
		GameManager.instance.gauntletRunning = false;
		GameManager.instance.lastBattlerIndex = -1;
	}

	[YarnCommand("startgauntlet")]
	public void StartGauntletRun(string gauntletName)//add the name of the gauntlet
	{
        currentGauntletName = gauntletName;
		GameManager.instance.gauntletRunning = true;
	}

	[YarnCommand("recorddefeat")]
	public void RecordDefeat(string enemyName)
	{
		for (int i = 0; i < GameManager.instance.enemiesDefeatedNames.Count; i++)
		{
			if (enemyName == GameManager.instance.enemiesDefeatedNames[i])
			{
                //check if they have defeated gauntlet already
                if (GameManager.instance.enemiesDefeatedTracker[i] == true)
                {
                    //already defeated gaunlet
                    //Add anylytics of repeat beat gauntlet
                    repeatBeatGauntletTracker.TriggerEvent();
                }
                else
                {
                    //first time defeating the gaunlet

                    //Add anylytics beat gauntlet of enemyName
                    beatGauntletTracker.TriggerEvent();
                }

                GameManager.instance.enemiesDefeatedTracker[i] = true;
				print(enemyName + " set to true in enemiesDefeatedTracker");
                
                
            }
        }
        

	}

	[YarnCommand("callcredits")]
	public void CallCredits()
	{
		SceneFlow.RunScene(SceneList.Credits);
	}

	[YarnCommand("setBeard")]
	public void Setbeard(string state){
		beard.SetActive(true);
	}
	#endregion


	#region yarnGauntletManager


	private void Start()
	{
		int currentBattler = GameManager.instance.lastBattlerIndex;
		print("current int: " + currentBattler.ToString());
		string nodeToCall = "";
		if (GameManager.instance.playerHealth == 0)
		{
			print("Player Lost");
			nodeToCall = GameManager.instance.enemyList[currentBattler].nodePlayerLoss;
		}
		else if (GameManager.instance.gauntletRunning)
		{
			nodeToCall = GameManager.instance.enemyList[currentBattler].nodePlayerWin;

		}
		print("nodeToCall: " + nodeToCall);
		if (nodeToCall != "")
		{
			StartCoroutine(CallDialogueAfterDelay(nodeToCall));
		}
		GameManager.instance.waitTime = 1f;
	}

	IEnumerator CallDialogueAfterDelay(string nodeToCall)
	{
		yield return new WaitForSeconds(GameManager.instance.waitTime);
		FindObjectOfType<DialogueRunner>().StartDialogue(nodeToCall);
	}
    #endregion

    #region Analytics

    [YarnCommand("beatbattle")]
    public void BeatBattleAnylitcs(string battleNum,string gauntletName)
    {
        //do stuff
    }

    #endregion
}
