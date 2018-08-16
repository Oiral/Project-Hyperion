﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{

    public float speed;
    public float walkSoundDelay;
    private Rigidbody rb;
    private bool walking;
    public AudioClip walkSound;
    private AudioSource source;
    private float volLowRange = 0.5f;
    private float volHighRange = 1.0f;
    public bool walkingBool;
    bool soundBool = true;
    public bool normal_ideal_bool;
    Animator m_Animator;
    bool m_Walk;

    public static PlayerController instance;

    public PlayerController Instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }

        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
        walkingBool = false;
        m_Animator = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");


        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.velocity = (movement.normalized * speed);

        rb.angularVelocity = Vector3.zero;

        m_Animator.SetBool("Walking Bool",walkingBool);

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            //Set our rotation to our move direction
            transform.rotation = Quaternion.LookRotation(rb.velocity, Vector3.up);

            walkingBool = true;
            Debug.Log("wB = true");
        }
        else
        {
            walkingBool = false;
            Debug.Log("wB = false");
        }

        if (walkingBool && soundBool)
        {
            float vol = Random.Range(volLowRange, volHighRange);
            soundBool = false;
            source.PlayOneShot(walkSound, vol);
            Debug.Log("sfx_playing");
            StartCoroutine(ResetSoundBool());
        }

        if (Input.GetKey("escape"))
        {
            SceneManager.LoadScene(6);
        }
    }
    IEnumerator ResetSoundBool()
    {
        yield return new WaitForSeconds(walkSoundDelay);
        soundBool = true;
        print("WB = false");
    }

    void WalkingBool()
    {

    }

    void idealBool()
    {
        normal_ideal_bool = true;
    }
}
