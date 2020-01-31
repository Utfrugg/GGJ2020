using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private float horizontal = 0.0f;
    private float vertical = 0.0f;
    private int playerNumber = 0;

    public float runSpeed = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playerNumber = GetComponent<PlayerController>().PlayerNumber;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal" + playerNumber);
        vertical = Input.GetAxisRaw("Vertical" + playerNumber);
    }

    void FixedUpdate()
    { 

        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }
}
