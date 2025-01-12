using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    private int playerScore = 0;
    private int highScore = 0;

    public AudioSource music;
    public AudioSource fruitCollectionNoise;
    public AudioSource gameOverBellNoise;

    private GameObject player;
    private PlayerScript playerScript;
    private BoxCollider2D playerBoxCollider;

    private GameObject musicManagerAudio;
    private GameObject fruitSpawner;

    private GameObject titleUI;
    private GameObject resultsUI;

    private GameObject newHighScore;
    private GameObject oldHighScore;
    private Text highScoreText;

    private Text bottomScore;
    private Text resultScore;

    private Animator timerBarAnimator;

    private TimerBarScript timerBarScript;

    private CameraMoveScript cameraMoveScript;

    private PowerupManagerScript powerupManagerScript;

    private bool isGamePlaying;

    void Start () {
        musicManagerAudio = GameObject.Find("Music Manager");

        fruitSpawner = GameObject.Find("Fruit Spawner");

        titleUI = GameObject.Find("Middle Screen");
        resultsUI = GameObject.Find("Results Screen");

        //Automatically switches to WebGL Title Screen if it can't find regualr title
        if (titleUI == null)
        {
            titleUI = GameObject.Find("Middle Screen WebGl");
        }

        player = GameObject.Find("Player");
        playerScript = player.GetComponent<PlayerScript>();
        playerBoxCollider = player.GetComponent<BoxCollider2D>();

        timerBarScript = GameObject.Find("Progress").GetComponent<TimerBarScript>();
        timerBarAnimator = GameObject.Find("Progress").GetComponent<Animator>();

        newHighScore = GameObject.Find("New High Score");
        oldHighScore = GameObject.Find("Old High Score");
        highScoreText = GameObject.Find("High Score").GetComponent<Text>();

        bottomScore = GameObject.Find("Bottom Score").GetComponent<Text>();
        resultScore = GameObject.Find("Results Score").GetComponent<Text>();

        cameraMoveScript = GameObject.Find("Main Camera").GetComponent<CameraMoveScript>();

        powerupManagerScript = GameObject.Find("Powerup Manager").GetComponent<PowerupManagerScript>();

        isGamePlaying = false;
        //Resets everything
        gameOver();
    }

    private void Update()
    {
        //If the player presses escape, end the game
        if (Input.GetKeyDown(KeyCode.Escape) && getIsGamePlaying())
        {
            gameOver();
        }
    }

    public void saveHighScore (int score) {
        PlayerPrefs.SetInt("highScore", score);

        PlayerPrefs.Save();
    }

    public int loadHighScore () {
        //Will get the saved high score. If not found, it defaults to 0.
        return PlayerPrefs.GetInt("highScore", 0);
    }

    public bool getIsGamePlaying () {
        return isGamePlaying;
    }

    public void loadScene (string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void quitGame () {
        Application.Quit();

        //Quits the game in WebGl
        Application.OpenURL("about:blank");
    }

    public void bombHit () {
        player.SetActive(false);
        
        gameOver();
    }

    //Starts the game
    public void gameStart () {
        //Starts the music
        musicManagerAudio.GetComponent<AudioSource>().Play(0);

        //Starts the fruit spawner
        fruitSpawner.SetActive(true);

        //Starts timer bar
        timerBarAnimator.enabled = true;
        timerBarAnimator.Play("Timer Bar", -1, 0);

        //Player control and collision
        playerScript.enabled = true;
        playerBoxCollider.enabled = true;

        isGamePlaying = true;
    }

    //Ends the game
    public void gameOver () {
        //Stop the music, play a bell noise, stop the fruit spawning, pause timer, stop player control and collsion, and fly up to results screen

        //Stops music and plays bell noise
        musicManagerAudio.GetComponent<AudioSource>().Stop();

        //Stops the fruit spawner
        fruitSpawner.SetActive(false);

        //Pause timer
        timerBarAnimator.enabled = false;

        //Player control and collision
        playerScript.enabled = false;
        playerBoxCollider.enabled = false;

        //If the player wasn;t destroyed by a bomb, have them play the idle animation
        if (player.activeInHierarchy) {
            player.GetComponent<Animator>().Play("playerIdle");
        }

        isGamePlaying = false;

        StartCoroutine(goToResults());
    }

    public void pause () {

    }

    public void unpause () {

    }

    public void addScore (int numPoints) {
        playerScore += numPoints;

        updateScoreUI();
    }

    public string beautifyScore (int score) {
        //Keeps the zeros in the displayed score
        string defaultZeros = "000000";
        string scoreString = score.ToString();
        string fullScore = defaultZeros.Substring(scoreString.Length) + scoreString;

        return fullScore;
    }

    public void updateScoreUI () {
        bottomScore.text = beautifyScore(playerScore);
    }

    public void setNewHighScore () {
        highScore = loadHighScore();

        //If the new score is greater, update high score and tell player
        if (playerScore > highScore) {
            saveHighScore(playerScore);

            newHighScore.SetActive(true);
            oldHighScore.SetActive(false);
        }
        //Otherwise, just display old high score
        else {
            newHighScore.SetActive(false);
            oldHighScore.SetActive(true);

            highScoreText.text = beautifyScore(highScore);
        }
    }

    IEnumerator goToResults () {
        //Only goes to result screen if the gameOver function is run at the bottom screen
        if (cameraMoveScript.getCurrentPoint() != 0) {
            yield break;
        }
        
        //Wait a little before scrolling to the results screen
        yield return new WaitForSeconds(2);

        setNewHighScore();

        titleUI.SetActive(false);

        //Undoes the results button selections from previous games
        resultsUI.SetActive(false);
        resultsUI.SetActive(true);

        //Sets the top score
        resultScore.text = bottomScore.text;

        //Only scroll to results if the gameOver function ran at the bottom screen
        cameraMoveScript.setEndPoint(2);
    }

    public void goToTitle () {
        titleUI.SetActive(true);

        cameraMoveScript.setEndPoint(1);
    }

    public void goToMainGame () {
        //Resets the player
        player.SetActive(true);
        //Moves the player back to the center
        player.transform.position += new Vector3(-player.transform.position.x, 0, 0);

        //Resets score
        playerScore = 0;
        updateScoreUI();

        //Resets timer bar
        timerBarScript.reset();

        //Resets powerups
        powerupManagerScript.resetPowerups();

        cameraMoveScript.setEndPoint(0);
    }
}