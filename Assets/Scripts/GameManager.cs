using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EnemyType { Normal,Boss,Teacher};

[System.Serializable]
public struct Enemy{
	public string name;
	public string nodePlayerWin;
	public string nodePlayerLoss;
	public int hp;
	public Deck deck;
}



public class GameManager : MonoBehaviour {

	#region GauntletTracker
	public List<string> enemiesDefeatedNames;
	public bool[] enemiesDefeatedTracker;

	public bool CheckGauntlets()
	{
		bool returnbool = true;
		foreach(bool enemyBeat in enemiesDefeatedTracker)
		{
			returnbool = returnbool && enemyBeat;
		}
		print(returnbool);
		return returnbool;

	}
	#region GauntletVariables
	public bool gauntletRunning = false;
	public int lastBattlerIndex;
	public List<Enemy> enemyList;
	#endregion

	#endregion

    public static GameManager instance;


    public int playerHealth = 15;

	GameObject player;

	#region PlayerPositionOnMap

	[System.Serializable]
	public struct SavedPos
	{
		public Vector3 pos;
		public Quaternion rot;
	}

	[Header("Save Position")]
	public SavedPos playerSavePos;
	#endregion


	#region BattleInfo
	//Info to pass to battle scene
	//[HideInInspector]
	public int enemyHealth;
    //[HideInInspector]
    public Deck enemyDeck;
	public Sprite enemyBust;
	public string enemyName;
	#endregion

	#region Fader
	[Header("CameraFadeManager")]
	public GameObject fader;
	[HideInInspector]
	public float waitTime = 0f;
	#endregion

	#region TalkIndicator
	[Header("Talk Indicator")]
	public GameObject talkIndicator;
	#endregion

	// Use this for initialization
	void Awake () {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        { Destroy(gameObject); }

        DontDestroyOnLoad(gameObject);
	}

	private void Start()
	{
		int len = enemiesDefeatedNames.Count;
		enemiesDefeatedTracker = new bool[len];
		lastBattlerIndex = -1;
		fader.SetActive(true);
	}

	public void DestroySelf()
	{
		Destroy(gameObject);
	}

	public Deck GetDeck()
    {
        return enemyDeck;
    }

	public void SetEnemyHP(int hp)
	{
		enemyHealth = Mathf.Max(1, hp);
	}

	public int GetEnemyHP()
    {
        return enemyHealth;
    }

    public bool IsPlayerDead()
    {
        if (playerHealth <= 0)
        {
            Debug.Log("You lose");
            return true;
        }
        else
        {
            Debug.Log("Player is alive");
            return false;
        }
    }

    public void LoadFight(Deck deckToFight,int hp)
    {
        enemyDeck = deckToFight;

        enemyHealth = hp;

		//load battle scene
		StartBattle();
    }

    #region testing stuff

    public void Fight(int battleNum)
    {
        //LoadFight(decksToFight[battleNum], enemyHP[battleNum]);
    }

	#endregion

	public void StartBattle()
	{
		SavePlayerPosition();
		SceneFlow.RunScene(SceneList.Battle);
	}

	public void SetEnemy(string name, YarnFunctionsManager YM)
	{
		foreach (Enemy enemy in enemyList)
		{
			if (enemy.name.ToLower() == name.ToLower())
			{
				lastBattlerIndex = enemyList.IndexOf(enemy);
				enemyHealth = enemy.hp;
				enemyDeck = enemy.deck;
				enemyName = enemy.name;

				enemyBust = YM.busts[lastBattlerIndex].bust;
				break;
			}
		}
	}

	public void SetPlayer(GameObject newPlayer)
	{
		player = newPlayer;
	}
	
	public void SavePlayerPosition()
	{
		playerSavePos.pos = player.transform.position;
		playerSavePos.rot = player.transform.rotation;
	}
}
