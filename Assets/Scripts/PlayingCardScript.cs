using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayingCardScript : MonoBehaviour {

    public Card info;

    public Text title;
    public Text desc;
    public Text number;

    public Vector3 targetPos;
    public float moveSpeed = 10;

    private void Start()
    {
        info.SpawnInMatchCard(title, number, desc);
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);
    }
}
