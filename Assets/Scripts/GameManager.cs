using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum EnemyType { Normal,Boss,Teacher};

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public int playerHealth = 15;

    public static int previousSceneIndex;
    //public int battlesLoaded = 0;
    public List<Deck> decksToFight = new List<Deck>();
    public List<int> enemyHP = new List<int>();

    public int fightSceneNumber;


    public int enemyHealth;
    public Deck enemyDeck;

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

    public Deck GetDeck()
    {
        return enemyDeck;
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
        SceneManager.LoadScene(fightSceneNumber);
    }

    #region testing stuff

    public void Fight(int battleNum)
    {
        LoadFight(decksToFight[battleNum], enemyHP[battleNum]);
    }
    
    #endregion
}
