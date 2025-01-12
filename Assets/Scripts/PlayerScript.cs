using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Animator playerAnimator;
    
    public float moveSpeed;

    //The animation state machine. The states must have the same name as the animation names
    private string currentState = "playerIdle";
    private const string PLAYER_IDLE = "playerIdle";
    private const string PLAYER_WALK = "playerWalk";
    

    // Start is called before the first frame update
    void Start () {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
        //Checks horizontal input every frame based off of the Input Manager
        float hInput = Input.GetAxis("Horizontal");

        //Moves the player left and right based off of input, speed, and amount of time passed
        gameObject.transform.Translate((hInput * moveSpeed * Time.deltaTime), 0f, 0f);

        //If moving, play walk animation
        if (hInput != 0) {
            changeAnimationState(PLAYER_WALK);
        }
        //Else, play idle animation
        else {
            changeAnimationState(PLAYER_IDLE);
        }
    }

    void changeAnimationState (string newState) {
        //Only play the new animation if the new animation is actually new
        if (newState != currentState) {
            currentState = newState;

            //Ends current animation and starts the new one
            playerAnimator.Play(currentState);
        }
    }
}
