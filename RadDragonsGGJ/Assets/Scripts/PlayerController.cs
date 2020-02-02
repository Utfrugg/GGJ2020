using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerInteraction))]

public class PlayerController : MonoBehaviour
{

    public int PlayerNumber = 0;
    bool buttonHeld = false;

    //TODO hook up with UI
    private float oxygen = 1.0f;
    public float oxygenLoss = 0.01f;
    public bool losingOxygen = false;

    public bool dead { get; set; } = false;

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            if (losingOxygen)
            {
                oxygen -= oxygenLoss;

                if (oxygen <= 0.0f)
                {
                    Die();
                }
            }
            else
            {
                if (oxygen >= 1.0f)
                {
                    oxygen = 1.0f;
                }
                else
                {
                    oxygen += oxygenLoss;
                }
            }


            GetComponent<PlayerInteraction>().UpdateTarget(transform.position);

            if (Input.GetKeyDown("joystick " + PlayerNumber + " button 0"))
            {
                GetComponent<PlayerInteraction>().OnPickupPress();
            }
            if (Input.GetKeyDown("joystick " + PlayerNumber + " button 2"))
            {
                GetComponent<PlayerInteraction>().OnInteractPress();
                GetComponent<PlayerInteraction>().isHoldingButton = true;
            }
            if (Input.GetKeyUp("joystick " + PlayerNumber + " button 2"))
            {
                GetComponent<PlayerInteraction>().isHoldingButton = false;
            }

            if (Input.GetKeyDown("joystick " + PlayerNumber + " button 3"))
            {
                GetComponent<PlayerInteraction>().OnThrow();
            }
        }
        
        //Debug.DrawLine(transform.position, transform.position + transform.up, Color.red, 0.1f);
    }

    public void Die()
    {
        dead = true;
        GetComponent<PlayerMovement>().canMove = false;
        transform.Find("Character").GetComponent<SpriteRenderer>().color = Color.red;
        if (GetComponent<PlayerInteraction>().pickup != null)
        {
            GetComponent<PlayerInteraction>().pickup.OnDrop();
            GetComponent<PlayerInteraction>().pickup = null;
        }
    }
}
