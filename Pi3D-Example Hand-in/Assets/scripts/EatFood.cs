using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class for the food behavior. Very simple, as the food only change the score of the player.
/// </summary>
public class EatFood : MonoBehaviour
{
    //Again find the manager
    private GameObject gameManager;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    void OnCollisionEnter(Collision collision)
    {

        // If the player collides with the food - add to the score, change the UI, destroy the food and spawn a new one.
        // Be careful that you only have one main collider on the player that can interact with the food, as multiple spawn do not occur.
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("CRUNCH");


            gameManager.GetComponent<ObjectSpawner>().spawnCheese();

            ScoreKeeper.foodScore += 1;

            gameManager.GetComponent<ChangeScores>().changeFoodText();

            Destroy(gameObject);


        }

    }



}
