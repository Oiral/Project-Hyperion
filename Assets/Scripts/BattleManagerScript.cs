using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManagerScript : MonoBehaviour {

	

    public PlayerScript enemy;
    public PlayerScript player;
    public GameObject cardPrefab;

    //UI Stuff
    public GameObject UiCardPrefab;
    public GameObject UIHandParent;
    public List<GameCard> playerHand = new List<GameCard>();

    public GameCard[] enemyCardsInPlay = new GameCard[3];
    public GameCard[] playerCardsInPlay = new GameCard[3];

    int AiXValue = 0;
    int PlayerXValue = -20;

    #region testing
    //Testing Items
    public Button setupUpTurnButton;
    public Button playerAutoPlayButton;
    public Button matchButton;

    public void StartTurn()
    {
        //setupUpTurnButton.interactable = false;
        StartCoroutine(SetUpTurn());
        playerAutoPlayButton.interactable = true;
    }
    public void PlayerAutoPlay()
    {
        //playerAutoPlayButton.interactable = false;
        //StartCoroutine(PlayerSetUpTurnAuto());
        matchButton.interactable = true;
    }
    public void MatchCards()
    {
        //matchButton.interactable = false;
        StartCoroutine(CheckCards());
    }


    private void OnDisable()
    {
        enemy.currentDeck.activeDeck.Clear();
        player.currentDeck.activeDeck.Clear();
    }

    IEnumerator PlayerSetUpTurnAuto()
    {
        yield return new WaitForSeconds(0.1f);
        PlayerDraw();
    }

    #endregion

    IEnumerator SetUpTurn()
    {
        yield return new WaitForSeconds(0.1f);
        AiDraw();
        PlayerDraw();
    }

    public void AiDraw()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject SpawnedCardObject = Instantiate(cardPrefab);
            GameCard spawnedCard = SpawnedCardObject.GetComponent<PlayingCardScript>().info = enemy.currentDeck.DrawRandom();
            spawnedCard.attachedObject = SpawnedCardObject;
            SpawnedCardObject.GetComponent<PlayingCardScript>().targetPos = new Vector3(AiXValue, 0, i * 12);
            enemyCardsInPlay[i] = spawnedCard;
            //Debug.Log("spawned card", spawnedCard.attachedObject);
        }
    }

    public void PlayerDraw()
    {
        while (playerHand.Count < 5)
        {
            //Check if there is more card to draw
            if (player.currentDeck.activeDeck.Count > 0)
            {

                GameObject UISpawnedCardObject = Instantiate(UiCardPrefab, UIHandParent.transform);
                GameCard spawnedCard = UISpawnedCardObject.GetComponent<UiCardScript>().info = player.currentDeck.DrawRandom();
                spawnedCard.attachedObject = UISpawnedCardObject;
                playerHand.Add(spawnedCard);
            }
            else
            {
                break;
            }
        }
    }

    public void PlayerPlayCard(GameCard cardPlayed)
    {
        for (int i = 0; i < 3; i++)
        {
            if (playerCardsInPlay[i] == null)
            {
                //Destroy the card played
                Destroy(cardPlayed.attachedObject);
                playerHand.Remove(cardPlayed);

                GameObject SpawnedCardObject = Instantiate(cardPrefab);
                SpawnedCardObject.GetComponent<PlayingCardScript>().info = cardPlayed;
                cardPlayed.attachedObject = SpawnedCardObject;
                SpawnedCardObject.GetComponent<PlayingCardScript>().targetPos = new Vector3(PlayerXValue, 0, i * 12);
                playerCardsInPlay[i] = cardPlayed;
                break;
            }
        }
    }

    IEnumerator CheckCards()
    {
        EvaluateRow(0);
        yield return new WaitForSeconds(1);
        EvaluateRow(1);
        yield return new WaitForSeconds(1);
        EvaluateRow(2);
        yield return new WaitForSeconds(1);
        StartCoroutine(EndTurn());
    }

    public void EvaluateRow(int row)
    {
        //Debug.Log(playerCardsInPlay[row].nameOfCard + " + " + enemyCardsInPlay[row].nameOfCard);
       

        //Check if the players card should go first depending on what type of card it is
        if (playerCardsInPlay[row].typeOfCard < enemyCardsInPlay[row].typeOfCard)
        {
            EvaluateCard(playerCardsInPlay[row],row,true);
            EvaluateCard(enemyCardsInPlay[row], row, false);
        }
        else
        {
            EvaluateCard(enemyCardsInPlay[row], row, false);
            EvaluateCard(playerCardsInPlay[row], row, true);
        }
        playerCardsInPlay[row] = enemyCardsInPlay[row] = null;
    }

    public void EvaluateCard(GameCard cardToEval,int row,bool isPlayer)
    {
        //Check if the card is disabled
        if (cardToEval.enabled)
        {
            GameCard[] myCollumn;
            GameCard[] otherCollumn;

            PlayerScript user;
            PlayerScript opponent;

            if (isPlayer)
            {
                myCollumn = playerCardsInPlay;
                otherCollumn = enemyCardsInPlay;
                user = player;
                opponent = enemy;
            }
            else
            {
                myCollumn = enemyCardsInPlay;
                otherCollumn = playerCardsInPlay;
                user = enemy;
                opponent = player;
            }

            switch (cardToEval.typeOfCard)
            {
                case CardType.Mirror:
                    Debug.Log("Mirroring");
                    if (enemyCardsInPlay[row].relatedCard == playerCardsInPlay[row].relatedCard)//Checking for the edge case of both being a mirror
                    {
                        break;
                    }

                    EvaluateCard(otherCollumn[row], row, isPlayer);

                    //Visual stuff
                    Vector3 currpos = cardToEval.attachedObject.GetComponent<PlayingCardScript>().targetPos;

                    //spawn in the mirrored card
                    Card relatedCard = otherCollumn[row].relatedCard;
                    GameObject SpawnedCardObject = Instantiate(cardPrefab,otherCollumn[row].attachedObject.transform.position,Quaternion.identity);
                    GameCard spawnedCard = SpawnedCardObject.GetComponent<PlayingCardScript>().info = otherCollumn[row];
                    spawnedCard.attachedObject = SpawnedCardObject;
                    SpawnedCardObject.GetComponent<PlayingCardScript>().targetPos = currpos;
                    myCollumn[row] = spawnedCard;

                    //move the mirror card down
                    
                    currpos.y -= 1;
                    cardToEval.attachedObject.GetComponent<PlayingCardScript>().targetPos = currpos;
                    break;

                case CardType.Freeze:
                    Debug.Log("Freeze");
                    if (otherCollumn[row].extras != CardFamily.Effect)
                    {
                        otherCollumn[row].enabled = false;
                    }
                    break;

                case CardType.Thief:
                    Debug.Log("Thief");
                    if (otherCollumn[row].extras == CardFamily.Effect)//Check if opponents card is a effect
                    {
                        //If they are both thiefs disable both
                        //enemyCardsInPlay[row].enabled = playerCardsInPlay[row].enabled = false;
                        break;
                    }

                    //Swap the other cards around
                    myCollumn[row] = otherCollumn[row];
                    otherCollumn[row] = cardToEval;

                    //Disable this card
                    cardToEval.enabled = false;
                    cardToEval.attachedObject.GetComponent<PlayingCardScript>().targetPos += new Vector3(0, -1, 0);
                    EvaluateCard(myCollumn[row], row, isPlayer);

                    //visualization
                    UpdateCardPos();
                    break;

                case CardType.Multiply:
                    Debug.Log("Mulitply");
                    if (row >= 2)//Catch if this multiply is the last one in the list
                    {
                        break;
                    }

                    myCollumn[row + 1].multiplyValue += 1;

                    //visualisation
                    cardToEval.attachedObject.GetComponent<PlayingCardScript>().targetPos += new Vector3(0, -1, 9);
                    break;
                case CardType.Heal:
                    Debug.Log("Heal");
                    //heal the player
                    user.Heal(cardToEval.attackDamage * cardToEval.multiplyValue);
                    break;
                case CardType.Block:
                    Debug.Log("Block");
                    //add block
                    user.Block(cardToEval.attackDamage * cardToEval.multiplyValue);
                    break;
                case CardType.Hit:
                    Debug.Log("Hit");
                    //deal damage
                    opponent.Damage(cardToEval.attackDamage * cardToEval.multiplyValue);
                    break;
                case CardType.Chain:
                    Debug.Log("Chain");
                    //deal damage
                    opponent.Damage(cardToEval.attackDamage * cardToEval.multiplyValue);
                    break;
            }
        }
        
    }

    void UpdateCardPos()
    {
        foreach (GameCard curCard in playerCardsInPlay)
        {
            if (curCard != null)
            {
                Debug.Log("Updating card", curCard.attachedObject);
                Vector3 pos = curCard.attachedObject.transform.position;
                pos.x = PlayerXValue;
                curCard.attachedObject.GetComponent<PlayingCardScript>().targetPos = pos;
            }
        }
        foreach (GameCard curCard in enemyCardsInPlay)
        {
            if (curCard != null)
            {
                Vector3 pos = curCard.attachedObject.transform.position;
                pos.x = AiXValue;
                curCard.attachedObject.GetComponent<PlayingCardScript>().targetPos = pos;
            }
        }
    }


    IEnumerator EndTurn()
    {
        //move cards that are in play
        foreach (GameObject cardInPlay in GameObject.FindGameObjectsWithTag("Played Card"))
        {
            cardInPlay.GetComponent<PlayingCardScript>().targetPos += new Vector3(0, -50, 0);
            Destroy(cardInPlay, 2);
        }
        yield return new WaitForSeconds(1);
        //Check if there are no more cards left
        if (player.currentDeck.activeDeck.Count > 0)
        {
            StartCoroutine(SetUpTurn());
            playerAutoPlayButton.interactable = true;
        }
        else
        {
            Debug.Log("End Game!");
        }

        
    }

}
