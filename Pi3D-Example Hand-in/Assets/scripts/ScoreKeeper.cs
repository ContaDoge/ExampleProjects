using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for keeping the scores. It uses static variables for the food and lives management
/// In a better version with more game variables the score keeper can be set as a singleton or as a serialized object, as we only need 1 instance of it
/// </summary>
public class ScoreKeeper : MonoBehaviour
{
    //Food score - starts as a 0
    public static int foodScore = 0;
    //Lives score - starts with 3 lives
    public static int livesScore = 3;

    //Object that will contain the reference to the gameManager object to invoke the end game visuals
    private GameObject gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        //If lives are equal or less than zero show end game text and score
        if (livesScore <= 0)
        {
            gameManager.GetComponent<ChangeScores>().showEndGame();
        }
    }
}
