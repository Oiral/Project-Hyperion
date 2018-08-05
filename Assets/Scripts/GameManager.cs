using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public int playerHealth = 15;

    public static int previousSceneIndex;
    public int battlesLoaded = 0;
    public List<Deck> decksToFight = new List<Deck>();
    public List<int> enemyHP = new List<int>();

    public int fightSceneNumber;

	// Use this for initialization
	void Awake () {
        if (instance == null)
        {
            instance = this;
        }

        else if (instance != this)
        { Destroy(gameObject); }
        DontDestroyOnLoad(gameObject);
        previousSceneIndex = 0;
        SceneManager.sceneLoaded += OnSceneLoad;
	}

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void LoadFight()
    {
        battlesLoaded += 1;
        SceneManager.LoadScene(fightSceneNumber);
    }

    public Deck GetDeck()
    {
        return decksToFight[battlesLoaded - 1];
    }

    public int GetEnemyHP()
    {
        return enemyHP[battlesLoaded - 1];
    }

    void OnSceneLoad(Scene scene,LoadSceneMode mode)
    {
        if (playerHealth <= 0)
        {
            Debug.Log("You lose");
        }

        if (battlesLoaded >= decksToFight.Count)
        {
            Debug.Log("You win the fights!");
        }
    }
}
