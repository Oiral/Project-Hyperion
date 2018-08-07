using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShopScript : MonoBehaviour {

    public Vector3 targetPos;

    public float moveSpeed = 10;

    private void Start()
    {
        targetPos = transform.position;
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * moveSpeed);
    }
}
