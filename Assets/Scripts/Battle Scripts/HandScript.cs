using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour {

    public List<GameObject> itemsInHand;

    private void Start()
    {
        for (int i = 0; i < itemsInHand.Count; i++)
        {
            //x goes between -1 and 1
            float x = (float)i / ((float)itemsInHand.Count - 1);
            x = (x - 0.5f) * 2;

            itemsInHand[i].transform.position = transform.position;
            itemsInHand[i].transform.position += new Vector3(x * 50, 0);
            itemsInHand[i].transform.rotation = Quaternion.Euler(new Vector3(0, 0, x * -40));

            //if the card is being hovered over
            if (itemsInHand[i].GetComponent<UiCardScript>().hover)
            {
                itemsInHand[i].transform.localScale = new Vector3(2, 2, 2);
                itemsInHand[i].transform.position += new Vector3(0, 50);
            }
            else
            {
                itemsInHand[i].transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private void Update()
    {
        Start();
        
    }
}
