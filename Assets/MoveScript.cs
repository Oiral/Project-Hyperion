using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScript : MonoBehaviour {

    public Vector3 targetPos;
    public float moveSpeed = 10;

    public Vector2 targetSize;

    RectTransform rect;

    private void Start()
    {
        rect = GetComponent<RectTransform>();

        targetSize = rect.sizeDelta;
    }

    // Update is called once per frame
    void Update () {
        
        //transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
        //modify the position
        rect.position = Vector3.Lerp(rect.position, targetPos, moveSpeed * Time.deltaTime);

        //modify the size
        rect.sizeDelta = Vector2.Lerp(rect.sizeDelta, targetSize, moveSpeed * Time.deltaTime);
    }
}
