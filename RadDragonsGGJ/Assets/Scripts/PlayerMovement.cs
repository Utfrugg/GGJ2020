﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D body;
    private float horizontal = 0.0f;
    private float vertical = 0.0f;
    private int playerNumber = 0;

    public float runSpeed = 20.0f;
 
    public bool canMove { get; set; } = true;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playerNumber = GetComponent<PlayerController>().PlayerNumber;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            horizontal = Input.GetAxisRaw("Horizontal" + playerNumber);
            vertical = Input.GetAxisRaw("Vertical" + playerNumber);

            if (Input.GetKeyDown("joystick " + playerNumber + " button 0"))
            {
                Debug.Log("pickup");
            }
        }

    }

    void FixedUpdate()
    {
        if (canMove)
        {
            body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);

            if (!Mathf.Approximately(horizontal, 0.0f) || !Mathf.Approximately(vertical, 0.0f))
            {
                float angle = Mathf.Atan2(-horizontal, vertical) * Mathf.Rad2Deg;
                body.SetRotation(angle);
            }
        }
    }
}
