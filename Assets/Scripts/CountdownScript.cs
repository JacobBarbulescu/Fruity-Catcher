using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownScript : MonoBehaviour
{
    private Image image;

    private Animator animator;

    private GameManagerScript gameManagerScript;

    void Start () {
        animator = GetComponent<Animator>();

        image = GetComponent<Image>();

        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManagerScript>();
    }

    //Starts the game when the countdown is over
    public void startGame () {
        image.enabled = false;

        //If game isn't playing, run gameStart
        if (!gameManagerScript.getIsGamePlaying()) {
            gameManagerScript.gameStart();
        }
        //Else, the game is paused, so unpause the game
        else {
            gameManagerScript.unpause();
        }
    }

    public void startCountdown () {
        image.enabled = true;

        //Plays the countdown at layer -1 and at time 0. By setting the time to 0, we reset the animation every time we play it
        animator.Play("Countdown", -1, 0f);
    }
}
