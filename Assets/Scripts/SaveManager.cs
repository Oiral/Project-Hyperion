using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 
 * The save file works by breaking itself up into sections with 
 * 
 *					++++
 * 
 * If you want to add to the save file, create a section like this.
 * 
 * Each section is split between a header string and data
 * The section should look like: 
 * 
 *					"[Header]|||[Data]"
 *					
 * For example:
 * 
 *					Deck|||
 *					++++
 *					Enemies_defeated|||1010
 * 
 * Happy editing!
 */

public class SaveManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
