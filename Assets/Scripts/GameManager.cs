using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EnemyType { Normal,Boss,Teacher};

[System.Serializable]
public struct Enemy{
	public string name;
	public string nodeToFollow;
	public int hp;
	public Deck deck;
}



public class GameManager : MonoBehaviour {

	#region GauntletTracker
	public List<string> enemiesDefeatedNames;
	public bool[] enemiesDefeatedTracker;
	#endregion

	
	[System.Serializable]
	public struct SavedPos
	{
		public Vector3 pos;
		public Quaternion rot;
	}

    public static GameManager instance;


    public int playerHealth = 15;

	public static int previousSceneIndex;

	GameObject player;

	[Header("Save Position")]
	public SavedPos playerSavePos;
	//public int battlesLoaded = 0;
	//public List<Deck> decksToFight = new List<Deck>();
	//public List<int> enemyHP = new List<int>();


	//info kept safe for load back into main scene
	[Header("Safe keeping for main scene")]
    public int playersThroughGauntlet;
    public Dictionary<int, bool> gauntletNum = new Dictionary<int, bool>{
        { 0, false },
        { 1, false },
        { 2, false },
        { 3, false }
    };



    //Info to pass to battle scene
    //[HideInInspector]
    public int enemyHealth;
    //[HideInInspector]
    public Deck enemyDeck;

	public bool gauntletRunning = false;
	public int lastBattlerIndex;
	public List<Enemy> enemyList;

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

	public void SetEnemy(string name)
	{
		foreach (Enemy enemy in enemyList)
		{
			if (enemy.name.ToLower() == name.ToLower())
			{
				lastBattlerIndex = enemyList.IndexOf(enemy);
				enemyHealth = enemy.hp;
				enemyDeck = enemy.deck;
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
