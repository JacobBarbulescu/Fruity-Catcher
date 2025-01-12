using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupScript : MonoBehaviour
{
    //This handles administering the different powerups
    private PowerupManagerScript powerupManagerScript;
    private GameManagerScript gameManagerScript;
    private ItemExitScript itemExitScript;

    public SpriteRenderer powerupSpriteRenderer;
    public Rigidbody2D powerupRigidbody2D;
    public BoxCollider2D powerupBoxCollider2D;

    //These determine how potent each powerup is
    public float playerSpeedIncreaseAmount;
    public float playerSizeIncreaseAmount;
    public float itemsSizeIncreaseAmount;
    public float itemsSpeedDecreaseAmount;
    public float itemsSpawnRateDecreaseAmount;

    //In seconds
    public float powerupDuration;

    //This is the actual powerup type that the powerup will randomly be given
    //0- Faster player
    //1- Bigger player
    //2- Bigger items
    //3- Slower items
    //4- Faster item spawnning
    private int powerupType;

    // Start is called before the first frame update
    void Start()
    {
        powerupManagerScript = GameObject.Find("Powerup Manager").GetComponent<PowerupManagerScript>();
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManagerScript>();
        itemExitScript = GetComponent<ItemExitScript>();

        powerupType = powerupManagerScript.pickAvailablePowerup();

        //If there is no current powerup that this powerup can becone, it immediately dissapears
        if (powerupType == -1) {
            Destroy(gameObject);
            return;
        }

        //Makes it so that no duplicate powerups can spawn
        powerupManagerScript.setIsPowerupAvailable(powerupType, false);
    }

    void Update () {
        if (!gameManagerScript.getIsGamePlaying()) {
            Destroy(gameObject);
        }
    }

    //When powerup is destroyed, program makes sure that its corresponding ability is made available again
    void OnDestroy () {
        if (powerupType == -1) {
            return;
        }

        powerupManagerScript.setIsPowerupAvailable(powerupType, true);
    }

    //When the powerup is collected, it effectively switches to a timer, going invisible and disabling physics.
    //At the start, the powerup effect is applied to the corresponding object.
    //A coroutine begins that will last for however long the powerup effect lasts.
    //When the timer is up, the effect is undone and the powerup deletes itself.
    private void OnTriggerEnter2D(Collider2D other) {
        if ((itemExitScript.getCanCollideWithPlayer())&&(other.gameObject.tag == "Player")) {
            changeToTimer();

            //Faster player
            if (powerupType == 0) {
                StartCoroutine(fasterPlayerPowerup());
            }
            //Bigger player
            else if (powerupType == 1) {
                StartCoroutine(biggerPlayerPowerup());
            }
            //Bigger items
            else if (powerupType == 2) {
                StartCoroutine(biggerItemsPowerup());
            }
            //Slower items
            else if (powerupType == 3) {
                StartCoroutine(slowerItemsPowerup());
            }
            else if (powerupType == 4) {
                StartCoroutine(fasterItemsSpawns());
            }
        }
    }

    //This disables all physics and rendering of the powerup when collected
    //Makes it just handle the powerup aspect
    private void changeToTimer () {
        Destroy(powerupSpriteRenderer);
        Destroy(powerupRigidbody2D);
        Destroy(powerupBoxCollider2D);
    }

    //These all make the powerup change, wait for the powerup duration to end, then reverse action and kill themselves

    IEnumerator fasterPlayerPowerup () {
        powerupManagerScript.changePlayerSpeed(playerSpeedIncreaseAmount);
        yield return new WaitForSeconds(powerupDuration);
        powerupManagerScript.changePlayerSpeed(-playerSpeedIncreaseAmount);
        Destroy(gameObject);
    }

    IEnumerator biggerPlayerPowerup () {
        powerupManagerScript.changePlayerSize(playerSizeIncreaseAmount);
        yield return new WaitForSeconds(powerupDuration);
        powerupManagerScript.changePlayerSize(-playerSizeIncreaseAmount);
        Destroy(gameObject);
    }

    IEnumerator biggerItemsPowerup () {
        powerupManagerScript.changeItemsSize(itemsSizeIncreaseAmount);
        yield return new WaitForSeconds(powerupDuration);
        powerupManagerScript.changeItemsSize(-itemsSizeIncreaseAmount);
        Destroy(gameObject);
    }

    IEnumerator slowerItemsPowerup () {
        powerupManagerScript.changeItemsSpeed(itemsSpeedDecreaseAmount);
        yield return new WaitForSeconds(powerupDuration);
        powerupManagerScript.changeItemsSpeed(-itemsSpeedDecreaseAmount);
        Destroy(gameObject);
    }

    IEnumerator fasterItemsSpawns () {
        powerupManagerScript.changeItemsSpawnRates(itemsSpawnRateDecreaseAmount);
        yield return new WaitForSeconds(powerupDuration);
        powerupManagerScript.changeItemsSpawnRates(-itemsSpawnRateDecreaseAmount);
        Destroy(gameObject);
    }
}
