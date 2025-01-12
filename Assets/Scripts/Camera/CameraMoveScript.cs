using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMoveScript : MonoBehaviour
{
    private float endPoint;

    private float startPoint;

    public float movementLength = 3f;

    private float timeElapsed;

    private CountdownScript countdownScript;

    private GameManagerScript gameManagerScript;

    void Start () {
        startPoint = transform.position.y;
        endPoint = startPoint;
        timeElapsed = movementLength;

        countdownScript = GameObject.Find("Countdown").GetComponent<CountdownScript>();

        gameManagerScript = GameObject.Find("Game Manager").GetComponent<GameManagerScript>();
    }

    public float getCurrentPoint () {
        return transform.position.y;
    }

    public void setEndPoint (float newEndPoint) {
        startPoint = getCurrentPoint();
        
        endPoint = newEndPoint;
        timeElapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Has the camera curvily move to a given point, and, when reached, redisables the script.
        if (timeElapsed < movementLength) {
            timeElapsed += Time.deltaTime;

            //t moves from 0 to 1 over the length of the animation
            float t = timeElapsed/movementLength;
            
            //Smooth curve function from x = 0-1
            float result = t * t * (3f - (2f * t));

            //The -10 is the z value of the camera
            transform.position = new Vector3(0, Mathf.Lerp(startPoint, endPoint, result), -10);
        }
        //Makes it so this only happens once per slide
        else if (transform.position.y != endPoint) {
            //Locks the samera into the right place
            transform.position = new Vector3(0, endPoint, -10);

            //Deselects the button that was clicked (selected) to move the camera
            EventSystem.current.SetSelectedGameObject(null);

            //If slid to main game screen, start countdown
            if (endPoint == 0f) {
                countdownScript.startCountdown();
            }
        }
    }
}
