using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float startSpeed; //start speed of ball
    public float extraSpeed; //how much speed the ball increases after bounce off racket
    public float maxExtraSpeed; // max extra speed to prevent crazy fast speeds

    public bool player1Start = true; // want player 1 to start off with ball when true //? what is the point of this

    private int hitCounter = 0; //how many times the ball has been hit by the racket - this helps us increase speed continuously later

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //fetches the Rigidbody2D from our "Ball" game object --> applies the rigidbody physics of the ball to the rb variable
        StartCoroutine(Launch());
    }

    private void RestartBall()
    {
        rb.velocity = new Vector2(0,0); // stops ball from moving - sets velocity to 0
        transform.position = new Vector2(0,0); // ball moves back to center
    }

    public IEnumerator Launch()
    {
        RestartBall();
        hitCounter = 0; //Reset hit counter before launching the ball
        yield return new WaitForSeconds(1); // make the ball wait for a second before launching ball - to have the player get ready 
        MoveBall(new Vector2(-1, 0));

        if(player1Start == true) // if player1 is starting //? what does it mean for player 1 to be starting
        {
            MoveBall(new Vector2(-1,0)); // ball moves left
        }
        else // player2 is starting
        {
            MoveBall(new Vector2(1,0)); // ball moves right 
        }
    }
    
    public void MoveBall(Vector2 direction)
    {
        direction = direction.normalized; // Why does this need to be normalized - what does this mean to be normalized
        float ballSpeed = startSpeed + (hitCounter * extraSpeed); // increase the speed continuously
        rb.velocity = direction * ballSpeed;
    }

    public void IncreaseHitCounter()
    {
        if (hitCounter * extraSpeed < maxExtraSpeed)
        {
            hitCounter++;
        }
    }

}
