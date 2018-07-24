using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum cardType { damage, heal, block };

public class AttackCardScript : MonoBehaviour
{
    public Renderer cardRenderer;

    public cardType typeOfCard;

    public void OnClick()
    {
        Debug.Log("Attack!!!!");
        switch (typeOfCard)
        {
            case cardType.damage:
                GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
                enemy.GetComponent<EnemyScript>().ChangeHealth(-1);
                break;
            case cardType.heal:
                break;
            case cardType.block:
                break;
            default:
                break;
        }
    }
	
}
