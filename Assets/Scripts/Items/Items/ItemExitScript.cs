using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemExitScript : MonoBehaviour
{
    public Rigidbody2D itemRigidbody2D;

    private float maxHorizontalDistance;

    private float itemExitSpeed;

    private float graceTime;

    private bool canCollideWithPlayer = true;

    // Update is called once per frame
    void Update()
    {
        //If go out of horizontal range, self-destroy
        if (Math.Abs(gameObject.transform.position.x) > maxHorizontalDistance) {
            Destroy(gameObject);
        }
    }

    //Allows the fruit spawner to access this private property
    public bool getCanCollideWithPlayer () {
        return canCollideWithPlayer;
    }

    //Allows the fruit spawner to set these private properties
    public void setExitProperties (float horizontalDistance, float exitSpeed, float grace) {
        maxHorizontalDistance = horizontalDistance;
        itemExitSpeed = exitSpeed;
        graceTime = grace;
    }

    //When touch the ground, disable player collision and roll off to the side
    private void OnCollisionEnter2D (Collision2D other) {
        if (other.gameObject.tag == "Ground") {
            StartCoroutine(disablePlayerCollision());
            
            Vector3 itemExitVelocity;

            //If on right side, have powerup fly off to the right
            if (gameObject.transform.position.x >= 0) {
                itemExitVelocity = new Vector3(itemExitSpeed, itemRigidbody2D.velocity.y, 0);
            }
            //Else, have it fly off to the left
            else {
                itemExitVelocity = new Vector3(-itemExitSpeed, itemRigidbody2D.velocity.y, 0);
            }

            itemRigidbody2D.velocity = itemExitVelocity;
        }
    }

    //Gives a little time after hitting ground before diabling player collision
    //Makes the game seem more fair
    IEnumerator disablePlayerCollision () {
        yield return new WaitForSeconds(graceTime);

        canCollideWithPlayer = false;
    }
}
