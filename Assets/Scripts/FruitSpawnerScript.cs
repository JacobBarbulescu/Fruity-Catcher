using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSpawnerScript : MonoBehaviour
{
    public float maxSpawnDistance;

    public float minSpawnRate;
    public float maxSpawnRate;

    private float timer = 0;
    public float spawnRate = 3;

    public GameObject fruit;
    public GameObject bomb;
    public GameObject specialFruit;
    public GameObject powerup;

    //These are all percent chances that the new item will be one of these. All add up to 1
    public float fruitSpawnChance;
    public float bombSpawnChance;
    public float specialFruitSpawnChance;
    public float powerupSpawnChance;

    //Handles the common properties of the items to be spawned
    public float itemGravityScale;
    public float itemScale;
    public float itemMaxTorque;

    //Handles the properties of fruits/powerups after they hit the ground
    public float itemMaxHorizontalDistance;
    public float itemExitSpeed;
    public float itemGraceTime;

    //These are so we can find in what item's percent range the random percent falls in
    //Calculated automatically at start
    private float maxFruitSpawnPercent;
    private float minBombSpawnPercent;
    private float maxBombSpawnPercent;
    private float minSpecialFruitSpawnPercent;
    private float maxSpecialFruitSpawnPercent;
    private float minPowerupSpawnPercent;

    void Start () {
        calculateSpawnPercents();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > spawnRate) {
            spawnItem(pickItem());

            timer = 0;
            spawnRate = Random.Range(minSpawnRate, maxSpawnRate);
        }
    }

    public GameObject pickItem () {
        //Will generate a percentage from 0 to 1 and return an item type based off of the percentage

        //Random percent from 0 to 1 chosen
        float spawnPercent = Random.Range(0f, 1f);

        //fruit
        if (spawnPercent <= maxFruitSpawnPercent) {
            return fruit;
        }
        //bomb
        else if ((spawnPercent > minBombSpawnPercent)&&(spawnPercent <= maxBombSpawnPercent)) {
            return bomb;
        }
        //special fruit
        else if ((spawnPercent > minSpecialFruitSpawnPercent)&&(spawnPercent <= maxSpecialFruitSpawnPercent)) {
            return specialFruit;
        }
        //powerup
        else if (spawnPercent > minPowerupSpawnPercent) {
            return powerup;
        }

        //If percent is somehow not in the range, default to fruit
        return fruit;
    }

    private void spawnItem (GameObject newObject) {
        //Makes a new game object in the given range
        GameObject newItem = Instantiate(newObject, new Vector3(Random.Range(-maxSpawnDistance, maxSpawnDistance), gameObject.transform.position.y, gameObject.transform.position.z), Quaternion.identity);

        newItem.GetComponent<Rigidbody2D>().gravityScale = itemGravityScale;
        newItem.GetComponent<Transform>().localScale = new Vector3(itemScale, itemScale, 1);
        newItem.GetComponent<ItemScript>().applyTorque(itemMaxTorque);

        ItemExitScript newItemExitScript = newItem.GetComponent<ItemExitScript>();
        if (newItemExitScript != null) {
            newItemExitScript.setExitProperties(itemMaxHorizontalDistance, itemExitSpeed, itemGraceTime);
        }

        //PLAY SOUND HERE
    }

    private void calculateSpawnPercents () {
        //This calculates the ranges of a percentage from 0 to 1 that, when generated, will correspond to a certain item type
        maxFruitSpawnPercent = fruitSpawnChance;

        minBombSpawnPercent = fruitSpawnChance;
        maxBombSpawnPercent = minBombSpawnPercent + bombSpawnChance;

        minSpecialFruitSpawnPercent = maxBombSpawnPercent;
        maxSpecialFruitSpawnPercent = minSpecialFruitSpawnPercent + specialFruitSpawnChance;

        minPowerupSpawnPercent = maxSpecialFruitSpawnPercent;
    }
}
