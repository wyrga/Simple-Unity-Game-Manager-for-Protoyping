using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

    /**
     * This is a simple Game Manager for quick prototyping, works as long as your mechanics include lives and/or points 
     * Examples
     * if(GameManager.isObjectAlive)
     * GameManager.instance.LoseLife();
     **/



    //Modify your lives from unity editor
    public int lives = 3;
    
    //How many points you need to advance
    public int goal = 30;
    
    //Delay between restarts and new map loads
    public float resetDelay = 1f;
    
    //Keeps track of the points in your scene
    private int points;
    //Possible multiplier
    private int multiplier;

    //Checks if the controller character is alive
    public static bool isObjectAlive;

    //Drag'n'drop your lives text here in the editor
    public Text livesText;

    //Drag'n'drop your points text here in the editor
    public Text pointsText;

    //Drag'n'drop your multiplierText in the editor
    public Text multiplierText;

    //This GameObject will appear once lives reach 0, drag'n'drop the object you want to be displayed in the editor
    public GameObject gameOver;

    //This GameObject will appear once possible objective is reached 
    public GameObject youWon;

    //Drag'n'drop the controlled character / object here in the editor, please make it a prefab and remove from the scene
    public GameObject character;

    //Call for the game manager trough this instance
    public static GameManager instance = null;

    //An abusable clone of your dear character
    private GameObject cloneCharacter;

    /**
     * Checks if there is an instance of this game manager running, if not a new instance of this game manager is created.
     * Destroys the game manager if there is another game manager running, this should never happen though..
     **/
	void Start () {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        Setup();
	}

    /**
     * Setup for the scene
     **/
    public void Setup()
    {
        cloneCharacter = Instantiate(character, transform.position, Quaternion.identity) as GameObject;
        points = 0;
        multiplier = 1;
        isObjectAlive = true;
    }

    /**
     * If lives reach 0 the scene will reset with a slow-mo effect
     **/
    void CheckGameOver()
    {

        
        if (lives < 1)
        {
            gameOver.SetActive(true);
            Time.timeScale = .25f;
            Invoke("Reset", resetDelay);

        }
    }

    /**
     * If points reach certain, youWon gameobject will be activated, time will be slowed down and you will advance to the next level
     **/
    void CheckYouWon()
    {

        if (points >= goal)
        {
            youWon.SetActive(true);
            Time.timeScale = .25f;
            Invoke("NextLvl", resetDelay);
        }


    }

    /**
     * Resets the current   level
     **/
    void Reset()
    {
        Time.timeScale = 1f;

        Application.LoadLevel(Application.loadedLevel);
    }


    /**
     * Calls for the next scene
     */
    void NextLvl()
    {
        Time.timeScale = 1f;
        //Please replace "1" with the index of the next level
        Application.LoadLevel(1);
    }


    /**
     * Takes away one life, checks if the value reaches zero
     */
    public void LoseLife()
    {
        lives--;
        livesText.text = "Lives: " + lives;
        CheckGameOver();
              
    }

    /**
     * Add multiplier from any gameobject with a script with a call:
     * GameManager.instance.addMultiplier(2);
     * 
     **/
    public void addMultiplier(int multiplierInt)
    {
        multiplier+=multiplierInt ;
        //lazy stringbuilding
        multiplierText.text = "" + multiplier;

    }

    /**
     * Add points from any script with a call like this: 
     * GameManager.instance.addPoints(4);
     */
    public void addPoints(int pointsInt)
    {
        points += (pointsInt * multiplier);
        //lazy stringbuilding
        pointsText.text = "" + points;

    }
}
