using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public float racketSpeed; // determines player 1's racket speed

    private Rigidbody2D rb;
    private Vector2 racketDirection; //determines the direction of racket
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //fetches the Rigidbody2D from our "Player 1" game object --> applies the rigidbody physics of the ball to the rb variable
    }

    // Update is called once per frame
    // For anything that needs to be updated regularly (Example(s): timers, detection of inputs,etc.)
    void Update()
    {
        // we're getting the input from player 1 - the input we want is GetAxisRaw
        // GetAxisRaw: doesn't matter how hard or soft the player presses the button - simply takes the raw value 
        // Why we use vertical - the basic controls of unity for vertical are s,w or up,down
        float directionY = Input.GetAxisRaw("Vertical"); // checks if player 1 pressed a button - save this within a float variable

        racketDirection = new Vector2(0, directionY).normalized; // racket direction movement (detection of inputs)
    }

    // Called once per physics frame
    // Example: Anything that involves RigidBody
    private void FixedUpdate()
    {
        rb.velocity = racketDirection * racketSpeed; // applying racketSpeed variable to the physics aspect of our racket (Rigidbody2D)
    }
}
