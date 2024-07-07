using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** Color Themes
* Important Information
* ! Attention
* ? Question in Code (Make this bright orange)
* TODO: something I need to do
*/


// makes the ball bounce correctly
public class BallBounce : MonoBehaviour
{
    public GameObject hitSFX;
    //reference BallMovement script - because we want to use/reference IncreaseHitCounter
    public BallMovement ballMovement;
    // reference ScoreManager script
    public ScoreManager scoreManager;
    
    //figure out if ball should bounce upward or downward
    // we need to know 1. Ball Position 2. Racket Position 3. Racket Height
    private void Bounce(Collision2D collision) // ! the private method 'Bounce' is called whenever the ball collides with an object
    // this method calculates the direction the ball should bounce based on the collision
    {
        Vector3 ballPosition = transform.position; // stores the current position of the ball 
        //? why Vector3 - this is because the Transform component in Unity always uses Vector3 for 
        //? position, rotation, and scale - even in 2D games
        Vector3 racketPosition = collision.transform.position; // stores the position of the racket that the ball collided with
        float racketHeight = collision.collider.bounds.size.y; // stores the height of the racket to help calculate where the ball hit on the racket

        float positionX; // determines the horizontal direction the ball should move after hitting a racket

        if(collision.gameObject.name == "Player 1") // ! if the ball hits Player 1
        {
            positionX = 1; // ! the Ball will move to the right (positive X direction)
            // Note: only sets the direction the ball will move - NOT the speed too
        }

        else // ! if the ball hits Player 2
        {
            positionX = -1; // ! the Ball will move to the left (negative X direction)
            // Note: only sets the direction the ball will move - NOT the speed too

        }
        //* ballPosition.y - racketPosition.y = vertical distance
        float positionY = (ballPosition.y - racketPosition.y) / racketHeight; // determines the vertical direction of the ball's movement after it hits the racket
        // ballPosition.y: y-coordinate of the ball's position at the moment of collision
        // racketPosition.y: y-coordinate of the racket's position at the moment of collision
        // racketHeight: Height of racket - represents the size of the racket in the vertical direction
        //! ballPosition.y - racketPosition.y: calculates the vertical distance between the center of the racket and the point where the ball hit the racket
        /* Example:
        * If the ball hits center of racket ->  Value = 0
        * If the ball hits above center of racket ->  Value = positive (+)
        * If the ball hits below center of racket ->  Value = negative (-)
        */
        //! Dividing (vertical distance) / racketHeight: Result is normalized to a range between -0.5 and 0.5 (this is a range of the angled ball direction after hitting racket)
        //! This normalization ensures the vertical direction (positionY) is proportionate to where the ball hits the racket

        ballMovement.IncreaseHitCounter(); // keeps track of the # of times the ball has been hit
        ballMovement.MoveBall(new Vector2(positionX, positionY)); // called to apply the new direction to the ball
    }

    // checks if ball hits either Player1 or Player2 racket
    private void OnCollisionEnter2D(Collision2D collision) // called when ball collides with another object 
    {
        if(collision.gameObject.name == "Player 1"  || collision.gameObject.name == "Player 2") // checks if ball hits either Player1 or Player2 racket
        {
            Bounce(collision); // if yes -> Bounce method is called 
        }

        else if(collision.gameObject.name == "Right Border")
        {
            scoreManager.Player1Goal();
            ballMovement.player1Start = false;
            StartCoroutine(ballMovement.Launch()); //? why are we calling Start Coroutine here 
        }

        else if(collision.gameObject.name == "Left Border")
        {
            scoreManager.Player2Goal();
            ballMovement.player1Start = true;
            StartCoroutine(ballMovement.Launch()); //? why are we calling Start Coroutine here 
        }

        Instantiate(hitSFX, transform.position, transform.rotation); // creates this hitSFX object in game
    }
}
