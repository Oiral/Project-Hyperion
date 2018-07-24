using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseManagerScript : MonoBehaviour {

    public GameObject activeCard;
    public GameObject selectedCard;

    private void Update()
    {
        RaycastHit hit;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            Debug.DrawLine(ray.origin, hit.transform.position,Color.red);
            if (hit.transform.tag == "Card")
            {
                activeCard = hit.transform.gameObject;
            }
            else
            {
                activeCard = null;
            }
        }else
        {
            activeCard = null;
        }

        if (activeCard != null)
        {
            Vector3 cardpos = activeCard.transform.position;
            cardpos.y = 0.1f;


            if (Input.GetMouseButtonDown(0))
            {
                AttackCardScript cardScript = activeCard.GetComponent<AttackCardScript>();
                Material mat = cardScript.cardRenderer.material;
                mat.color = Color.red;
                cardScript.cardRenderer.material = mat;
                cardScript.OnClick();
                selectedCard = activeCard;
            }
            
        }
        if (selectedCard != null)
        {
            if (Input.GetMouseButtonUp(0))
            {
                AttackCardScript cardScript = selectedCard.GetComponent<AttackCardScript>();
                Material mat = cardScript.cardRenderer.material;
                mat.color = Color.white;
                cardScript.cardRenderer.material = mat;
            }
        }
    }
}
