using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

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

		GameManager.instance.SetPlayer(gameObject);
		if (GameManager.instance.playerSavePos.pos != Vector3.zero)
		{
			transform.position = GameManager.instance.playerSavePos.pos;
			transform.rotation = GameManager.instance.playerSavePos.rot;
		}
		GameManager.instance.SavePlayerPosition();
	}

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

		m_Animator.SetBool("Walking Bool", walkingBool);

		if (FindObjectOfType<DialogueRunner>().isDialogueRunning == false)
		{
			Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
			rb.velocity = (movement.normalized * speed);

			rb.angularVelocity = Vector3.zero;
		

			if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
			{
				//Set our rotation to our move direction
				transform.rotation = Quaternion.LookRotation(rb.velocity, Vector3.up);

				walkingBool = true;
				soundBool = true;
				//Debug.Log("wB = true");

				if (!source.isPlaying)
				{
					float vol = Random.Range(volLowRange, volHighRange);
					//soundBool = false;
					source.Play();
					soundBool = false;
				}

			}
			else
			{
				walkingBool = false;
				//Debug.Log("wB = false");
			}

		}
		else
		{
			walkingBool = false;
			//Debug.Log("wB = false");
			rb.angularVelocity = Vector3.zero;
		}


	}

   

    void idealBool()
    {
        normal_ideal_bool = true;
		
	}
}
