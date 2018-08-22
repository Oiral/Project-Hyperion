using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class RewardsScript : MonoBehaviour {


    public List<GameCard> rewards = new List<GameCard>();

    BattleManagerScript battleManager;

    public GameObject rewardCardPrefab;

    public GameObject rewardCardPanel;

    private void OnEnable()
    {
        battleManager = GameObject.FindGameObjectWithTag("Battle Manager").GetComponent<BattleManagerScript>();

        rewards = battleManager.enemy.currentDeck.GetRewardCards();


        //Spawn in each reward card
        for (int i = 0; i < rewards.Count; i++)
        {
            GameObject rewardCard = Instantiate(rewardCardPrefab, rewardCardPanel.transform);

            UiCardScript UICard = rewardCard.GetComponent<UiCardScript>();
            UICard.info = rewards[i];


            Button cardButton = rewardCard.GetComponent<Button>();

            cardButton.onClick.RemoveAllListeners();
            cardButton.onClick.AddListener(delegate { AddCard(UICard.info); });
        }
    }
    
    public void AddCard(GameCard card)
    {
        //Debug.Log(card.relatedCard.nameOfCard);
        battleManager.player.currentDeck.unactiveDeck.Add(card.relatedCard);
        SceneFlow.RunScene(SceneList.MainScene);
    }

}
