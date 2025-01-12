using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBarScript : MonoBehaviour
{
    public Sprite green;
    public Sprite yellow;
    public Sprite orange;
    public Sprite red;

    private Image barImage;

    private GameManagerScript gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        barImage = GetComponent<Image>();
        barImage.sprite = green;

        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManagerScript>();
    }

    public void setGreen () {
        barImage.sprite = green;
    }

    public void setYellow () {
        barImage.sprite = yellow;
    }

    public void setOrange () {
        barImage.sprite = orange;
    }

    public void setRed () {
        barImage.sprite = red;
    }

    public void endGame () {
        gameManagerScript.gameOver();
    }

    public void reset () {
        barImage.fillAmount = 1;
        barImage.sprite = green;
    }
}
