using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAIScript : MonoBehaviour {

    public int maxHealth;
    public int currentHealth;

    public BattleManagerScript managerScript;

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (damage <= 0)
        {
            managerScript.EndBattle();
        }
    }
}
