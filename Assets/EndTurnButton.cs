using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurnButton : MonoBehaviour {

	public void OnSelect()
    {
        GameObject.FindGameObjectWithTag("SelectionBox").GetComponent<MoveScript>().targetPos = GetComponent<RectTransform>().transform.position - new Vector3(0,40,0);
        GameObject.FindGameObjectWithTag("SelectionBox").GetComponent<MoveScript>().targetSize = new Vector2(114.7f, 50);
    }

    public void OnDeselect()
    {
        GameObject.FindGameObjectWithTag("SelectionBox").GetComponent<MoveScript>().targetSize = new Vector2(65.71f, 98.9f);
    }
}
