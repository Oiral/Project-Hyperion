using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GauntletScript : MonoBehaviour {

	[System.Serializable]
	public struct Enemy
	{
		public string name;
		public string talkToNode;
		public int health;
		public Deck deck;
	}

	GameManager gm;

	public List<Enemy> enemies;

    public int gauntletNum;

    public List<int> dependantGauntlets;

    private void Start()
    {
        gm = GameManager.instance;
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Gauntlet Activated");

        //check if the dependants have also been beat
        if (CheckDependants())
        {
            //check if the player has already completed the gauntlet
            if (gm.gauntletNum[gauntletNum] == false)
            {
                //check if the gauntlet is finished
                if (gm.playersThroughGauntlet < enemies.Count)
                {
                    gm.playerSavePos = other.gameObject.transform.position;

                    gm.enemyDeck = enemies[gm.playersThroughGauntlet].deck;
                    gm.enemyHealth = enemies[gm.playersThroughGauntlet].health;
                    gm.playersThroughGauntlet += 1;
                    SceneFlow.RunScene(SceneList.Battle);

                }
                else
                {
                    Debug.Log("Finished the gauntlet");
                    gm.playersThroughGauntlet = 0;
                    gm.gauntletNum[gauntletNum] = true;
                    gm.playerHealth = 15;
                }
            }
        }
    }

    bool CheckDependants()
    {
        bool canPlay = true;

        for (int i = 0; i < dependantGauntlets.Count; i++)
        {
            if (gm.gauntletNum[dependantGauntlets[i]] == false)
            {
                //if the previous gauntlet hasn't been beaten
                canPlay = false;
                Debug.Log("Not the right requirements");
                break;

            }
        }

        return canPlay;
    }
}
