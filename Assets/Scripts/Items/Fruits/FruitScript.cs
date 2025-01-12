using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitScript : MonoBehaviour
{
    private GameManagerScript gameManagerScript;
    private ItemExitScript itemExitScript;

    public int pointValue;

    // Start is called before the first frame update
    void Start () {
        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManagerScript>();
        itemExitScript = GetComponent<ItemExitScript>();
    }

    //When touch player, add points and self-destroy
    private void OnTriggerEnter2D (Collider2D other) {
        if ((itemExitScript.getCanCollideWithPlayer())&&(other.gameObject.tag == "Player")) {
            gameManagerScript.addScore(pointValue);

            Destroy(gameObject);
        }
    }
}
