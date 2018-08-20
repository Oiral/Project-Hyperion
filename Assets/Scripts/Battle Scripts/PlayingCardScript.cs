using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayingCardScript : MonoBehaviour {

    public GameCard info;

    public Text title;
    public Text desc;
    public Text number;
    public Image enableImage;
    public Image backgroundImage;

    public Vector3 targetPos;
    public float moveSpeed = 10;

    private void Start()
    {
        info.UpdateMatchCardUI(title, number, desc, backgroundImage);
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);
        info.UpdateMatchCardUI(title, number, desc);
        if (!info.enabled)
        {
            Color col = enableImage.color;
            col.a = 0.5f;
            enableImage.color = col;
        }
    }
}
