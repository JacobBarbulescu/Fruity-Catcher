using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    private GameManagerScript gameManagerScript;
    
    public Rigidbody2D bombBody;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManagerScript>();
    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.gameObject.tag == "Player") {
            gameManagerScript.bombHit();

            blowUp();
        }
    }

    //When touch the ground, blow up
    private void OnCollisionEnter2D (Collision2D other) {
        if (other.gameObject.tag == "Ground") {
            blowUp();
        }
    }

    public void blowUp () {
        //PLAY ANIMATION

        Destroy(gameObject);
    }
}
