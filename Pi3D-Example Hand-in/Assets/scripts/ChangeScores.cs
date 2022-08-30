using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
/// <summary>
/// Class used to update the visuals and UI items
/// </summary>
public class ChangeScores : MonoBehaviour
{
    //Serialization of these fields is done for easier setup from the Inspector. In a larger project these should be done through a function calling all children of a 
    //UI holder object, so it can be more easily expanded if new elements are added
    [SerializeField]
    private TextMeshProUGUI foodText;

    [SerializeField]
    private TextMeshProUGUI livesText;


    [SerializeField]
    private TextMeshProUGUI gameOverText;


    [SerializeField]
    private TextMeshProUGUI highScoreText;



    void Start()
    {
        //Initial initialization of GUI items and hiding the end game ones. It's better form to hide them with code than from the inspector
        changeLivesText();
        changeFoodText();


        gameOverText.enabled = false;
        highScoreText.enabled = false;
    }

    //Public function for changing the food text - uses the static ScoreKeeper food value
    public void changeFoodText()
    {
        foodText.text = "Food: " + ScoreKeeper.foodScore;
    }

    //Public function for changing the lives text - uses the static ScoreKeeper lives value
    public void changeLivesText()
    {
        livesText.text = "Lives: " + ScoreKeeper.livesScore;
    }

    //Function for  initializing the end game view. This can be incorporated better by having a Game States class which would
    //be in charge or which parts can be shown/hidden. It will make it also easier to "reset" the game
    public void showEndGame()
    {
        //Hide the lives and food UI and show the end game UI
        gameOverText.enabled = true;
        highScoreText.enabled = true;
        highScoreText.text = "High Score: " + ScoreKeeper.foodScore;

        foodText.enabled = false;
        livesText.enabled = false;
    }

}
