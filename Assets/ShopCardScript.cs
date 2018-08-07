﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopCardScript : MonoBehaviour {

    public GameCard info;

    public Text title;
    public Text desc;
    public Text number;
    public Text cost;

    public Vector3 targetPos;
    public float moveSpeed = 10;

    public ShopManagerScript shopScript;

    private void Start()
    {
        info.UpdateMatchCardUI(title, number, desc, cost);
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);
        
    }


    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(title.text);
            shopScript.GivePlayerCard(info);
            Destroy(gameObject);
        }
    }
}
