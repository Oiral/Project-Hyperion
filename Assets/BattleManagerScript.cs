using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManagerScript : MonoBehaviour {

	public void EndBattle()
    {
        SceneManager.LoadScene(1);
    }
}
