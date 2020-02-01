﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerInteraction))]

public class PlayerController : MonoBehaviour
{

    public int PlayerNumber = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<PlayerInteraction>().UpdateTarget(transform.position);

        if (Input.GetKeyDown("joystick " + PlayerNumber + " button 0"))
        {
            GetComponent<PlayerInteraction>().OnPickupPress();
        }
        if (Input.GetKeyDown("joystick " + PlayerNumber + " button 2"))
        {
            Debug.Log("mlem");
            GetComponent<PlayerInteraction>().OnInteractPress();
        }

        //Debug.DrawLine(transform.position, transform.position + transform.up, Color.red, 0.1f);
    }
}
