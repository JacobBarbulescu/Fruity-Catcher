using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManagerScript : MonoBehaviour
{
    public GameObject player;
    public GameObject fruitSpawner;

    private PlayerScript playerScript;
    private FruitSpawnerScript fruitSpawnerScript;

    private Transform playerTransform;

    private bool[] isPowerupAvailable = new bool[5];

    //Default values of variables changed by powerups
    private float defaultPlayerSpeed;
    private Vector3 defaultPlayerSize;
    private float defaultItemsSpeed;
    private float defaultItemsSize;
    private float defaultItemsSpawnRate;

    void Start () {
        playerScript = player.GetComponent<PlayerScript>();
        fruitSpawnerScript = fruitSpawner.GetComponent<FruitSpawnerScript>();

        playerTransform = player.GetComponent<Transform>();

        initializeIsPowerupAvailable();

        //Sets the default powerup values to the values when the game is first loaded
        defaultPlayerSpeed = playerScript.moveSpeed;
        defaultPlayerSize = playerTransform.localScale;
        defaultItemsSpeed = fruitSpawnerScript.itemGravityScale;
        defaultItemsSize = fruitSpawnerScript.itemScale;
        defaultItemsSpawnRate = fruitSpawnerScript.maxSpawnRate;
    }

    //Resets any values changed by powerups before a new game starts
    public void resetPowerups() {
        playerScript.moveSpeed = defaultPlayerSpeed;
        playerTransform.localScale = defaultPlayerSize;
        fruitSpawnerScript.itemGravityScale = defaultItemsSpeed;
        fruitSpawnerScript.itemScale = defaultItemsSize;
        fruitSpawnerScript.maxSpawnRate = defaultItemsSpawnRate;
    }

    //This sets every index of isPowerupActive to false
    private void initializeIsPowerupAvailable () {
        for (int x = 0; x < isPowerupAvailable.Length; x++) {
            isPowerupAvailable[x] = true;
        }
    }

    //This will find all of the powerups that are not currently active, put them into a list, and return a random value of that list
    public int pickAvailablePowerup () {
        //Creates a list whose size can be modified
        List<int> availablePowerups = new List<int>();

        for (int x = 0; x < isPowerupAvailable.Length; x++) {
            if (isPowerupAvailable[x]) {
                availablePowerups.Add(x);
            }
        }

        //If there are no available powerups, return -1
        if (availablePowerups.Count == 0) {
            return -1;
        }

        //Selects a random, available powerup
        return availablePowerups[Random.Range(0, availablePowerups.Count)];
    }

    //Will switch the bool state of the given index of isPowerupAvailable
    public void setIsPowerupAvailable (int index, bool newState) {
        isPowerupAvailable[index] = newState;
    }

    public void changePlayerSpeed (float changeAmount) {
        playerScript.moveSpeed += changeAmount;
    }

    public void changePlayerSize (float changeAmount) {
        playerTransform.localScale += new Vector3(changeAmount, changeAmount, 0);
    }

    public void changeItemsSpeed (float changeAmount) {
        fruitSpawnerScript.itemGravityScale -= changeAmount;
    }

    public void changeItemsSize (float changeAmount) {
        fruitSpawnerScript.itemScale += changeAmount;
    }

    public void changeItemsSpawnRates (float changeAmount) {
        fruitSpawnerScript.maxSpawnRate -= changeAmount;
    }
}
