using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");


        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rb.AddForce(movement * speed);

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            walkingBool = true;
            Debug.Log("wB = true");
        }

        if (walkingBool && soundBool)
        {
            float vol = Random.Range(volLowRange, volHighRange);
            soundBool = false;
            source.PlayOneShot(walkSound, vol);
            Debug.Log("sfx_playing");
            StartCoroutine(ResetSoundBool());
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
