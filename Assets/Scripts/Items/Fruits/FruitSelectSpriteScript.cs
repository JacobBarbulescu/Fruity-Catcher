using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitSelectSpriteScript : MonoBehaviour
{
    public Sprite[] fruitSprites;

    public SpriteRenderer fruitSpriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        //Sets the sprite of the fruit to any one of the fruit sprites
        fruitSpriteRenderer.sprite = fruitSprites[Random.Range(0, fruitSprites.Length)];
    }
}
