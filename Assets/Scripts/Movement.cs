using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Vector2 MovementVector;
    Rigidbody2D rb;
    [SerializeField]
    float speed;

    bool playingSound;

    Audiomanager audioManager;

    FixedJoystick joystick;
    bool IsMobile;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioManager = GameObject.Find("audio").GetComponent<Audiomanager>();

        if(audioManager.IsMobile)
        {
            joystick = GameObject.FindObjectOfType<FixedJoystick>();
            IsMobile = true;
        }
        else
        {
            Destroy( GameObject.FindObjectOfType<FixedJoystick>().gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        MovementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));


    }

    private void FixedUpdate()
    {


        if (MovementVector.magnitude > 0.99f)
        {
            rb.velocity = MovementVector * speed;
            if (!playingSound)
            {
                audioManager.PlaySound("Move");
                playingSound = true;
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
            if (playingSound)
            {
                audioManager.StopSound("Move");
                playingSound = false;
            }
        }


        if (IsMobile) 
        {
            if (new Vector2(joystick.Horizontal, joystick.Vertical).magnitude > 0.1f)
            {
                rb.velocity = new Vector2(joystick.Horizontal, joystick.Vertical) * speed;
                if (!playingSound)
                {
                    audioManager.PlaySound("Move");
                    playingSound = true;
                }
            }
            else
            {
                rb.velocity = Vector2.zero;
                if (playingSound)
                {
                    audioManager.StopSound("Move");
                    playingSound = false;
                }
            }
        }



    }
}
