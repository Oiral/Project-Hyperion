using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour {

    public int health;

    public Text healthText;

    public void ChangeHealth(int healthToChange)
    {
        health += healthToChange;
        if (health <= 0)
        {
            Debug.Log("Im dead");
            health = 0;
        }
        healthText.text = health + "/10";
    }
}
