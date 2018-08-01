using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManagerScript : MonoBehaviour {

	

    public PlayerScript enemy;
    public PlayerScript player;
    public GameObject cardPrefab;

    public GameCard[] enemyCardsInPlay = new GameCard[3];
    public GameCard[] playerCardsInPlay = new GameCard[3];

    public int AiXValue = 0;
    public int PlayerXValue = -20;

    #region testing
    //Testing Items
    public Button setupUpTurnButton;
    public Button playerAutoPlayButton;
    public Button matchButton;

    public void StartTurn()
    {
        setupUpTurnButton.interactable = false;
        StartCoroutine(SetUpTurn());
        playerAutoPlayButton.interactable = true;
    }
    public void PlayerAutoPlay()
    {
        playerAutoPlayButton.interactable = false;
        StartCoroutine(PlayerSetUpTurnAuto());
        matchButton.interactable = true;
    }
    public void MatchCards()
    {
        matchButton.interactable = false;
        StartCoroutine(CheckCards());
    }


    private void OnDisable()
    {
        enemy.currentDeck.activeDeck.Clear();
        player.currentDeck.activeDeck.Clear();
    }

    IEnumerator PlayerSetUpTurnAuto()
    {
        yield return new WaitForSeconds(1);
        PlayerDraw();
    }
    public void PlayerDraw()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject SpawnedCardObject = Instantiate(cardPrefab);
            GameCard spawnedCard = SpawnedCardObject.GetComponent<PlayingCardScript>().info = player.currentDeck.DrawRandom();
            spawnedCard.attachedObject = SpawnedCardObject;
            SpawnedCardObject.GetComponent<PlayingCardScript>().targetPos = new Vector3(-20, 0, i * 12);
            playerCardsInPlay[i] = spawnedCard;
        }
    }

    #endregion

    IEnumerator SetUpTurn()
    {
        yield return new WaitForSeconds(1);
        AiDraw();
    }

    public void AiDraw()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject SpawnedCardObject = Instantiate(cardPrefab);
            GameCard spawnedCard = SpawnedCardObject.GetComponent<PlayingCardScript>().info = enemy.currentDeck.DrawRandom();
            spawnedCard.attachedObject = SpawnedCardObject;
            SpawnedCardObject.GetComponent<PlayingCardScript>().targetPos = new Vector3(0, 0, i * 12);
            enemyCardsInPlay[i] = spawnedCard;
        }
    }

    IEnumerator CheckCards()
    {
        EvaluateRow(0);
        yield return new WaitForSeconds(1);
        EvaluateRow(1);
        yield return new WaitForSeconds(1);
        EvaluateRow(2);
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
                    if (enemyCardsInPlay[row] == playerCardsInPlay[row])//Checking for the edge case of both being a mirror
                    {
                        break;
                    }

                    EvaluateCard(otherCollumn[row], row, isPlayer);
                    
                    break;

                case CardType.Freeze:
                    Debug.Log("Freeze");
                    otherCollumn[row].enabled = false;
                    break;

                case CardType.Thief:
                    Debug.Log("Thief");
                    if (otherCollumn[row].extras == CardFamily.Effect)//Check if opponents card is a effect
                    {
                        //If they are both thiefs disable both
                        enemyCardsInPlay[row].enabled = playerCardsInPlay[row].enabled = false;
                        break;
                    }

                    //Swap the other cards around
                    myCollumn[row] = otherCollumn[row];
                    otherCollumn[row] = cardToEval;

                    //Disable this card
                    cardToEval.enabled = false;
                    EvaluateCard(otherCollumn[row], row, isPlayer);

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
        foreach (GameCard card in playerCardsInPlay)
        {
            Vector3 pos = card.attachedObject.transform.position;
            pos.x = PlayerXValue;
            card.attachedObject.GetComponent<PlayingCardScript>().targetPos = pos;
        }
        foreach (GameCard card in enemyCardsInPlay)
        {
            Vector3 pos = card.attachedObject.transform.position;
            pos.x = AiXValue;
            card.attachedObject.GetComponent<PlayingCardScript>().targetPos = pos;
        }
    }


}
