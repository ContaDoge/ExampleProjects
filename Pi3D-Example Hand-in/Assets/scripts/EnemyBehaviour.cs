using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class dictating the enemy bahavior. Currently it is quite simple, as enemies just move forward from the direction they are spawned
/// They only react to two types of collisions - to walls and to the player and they ignore collisions from each other and the cheese object
/// </summary>
public class EnemyBehaviour : MonoBehaviour
{
    //Initial speed of each enemy. This changes as time in the game passes. Currently it is public, but it can be made private and only accessed through the object instance
    public float speed = 5;
    //The enemy rigidbody reference
    private Rigidbody currRigidBody;
    //Reference to the gameManager. Again, the code can be optimized by having the gameManager as a singleton reference initialized at the start of the game
    //so it is not necessary to find it in each class instance
    private GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        //Get the rigidbody from the current enemy
        currRigidBody = transform.GetComponent<Rigidbody>();
        gameManager = GameObject.Find("GameManager");
        //Set which layers should be ignored for Physics collisions in the Physics Engine
        Physics.IgnoreLayerCollision(6, 6, true);
        Physics.IgnoreLayerCollision(6, 7, true);
    }

    // Using the FixedUpdate for changing the Rigidbody velocity - good practice so physics are not connect to the framerate
    void FixedUpdate()
    {
        currRigidBody.velocity = transform.forward * speed;
    }

    // On collision events with other objects with colliders/rigidbodies
    void OnCollisionEnter(Collision collision)
    {
        //If the object is a wall just destroy the enemy
        if (collision.gameObject.tag == "wall")
        {
            Destroy(gameObject);

        }

        //If the object is a player subtract from the player lives, update the lives visual and destroy the enemy
        if (collision.gameObject.tag == "Player")
        {
            ScoreKeeper.livesScore -= 1;

            gameManager.GetComponent<ChangeScores>().changeLivesText();

            Destroy(gameObject);

        }


    }
}
