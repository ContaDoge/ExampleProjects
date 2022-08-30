using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Class that takes care of the spawning of enemies and food.
/// This can be extended to also spawn the player to randomize their position, to spawn the terrain to randomize the layout, etc.
/// Currently the class contains a simple implementation of a Timer. This should be refactors to separate the timer object into its own Class.
/// The Timer class can then send events when specific times are achieved and other classes like the spawner can react to the events. 
/// In it's current form it is ok, as it is simple and we have all the timer related work connected to this class.
/// 
/// Another optimization that should be done is not having to Instantiate enemies and cheeses constantly but have them Instantiate in the start of the game
/// and then reuse them, showing them and hidding them as necessary
/// </summary>
public class ObjectSpawner : MonoBehaviour
{
    //Private object that contain instances to the ground and cheese
    private GameObject cheeseObj;
    private GameObject groundObj;

    // The prefab cheese object
    [SerializeField]
    private GameObject spawnableObj;

    // Padding variables so when we spawn the cheese it does not go inside the walls
    [SerializeField]
    private float padding= 2f;
    // Four enemy spawner objects, as the enemies spawn from the four directions of the arena
    [SerializeField]
    private GameObject[] enemySpawners;
    //Enemy prefab object
    [SerializeField]
    private GameObject spawnableEnemy;
    //Initial speed of the enemy
    [SerializeField]
    private float enemySpeed = 5;
    //When the time hits this value a new enemy is spawned. This changes through the game
    [SerializeField]
    private float timerEnemy = 3f;
    //Current elapsed time
    private float elapsedTime = 0f;

    //The player object reference
    private GameObject playerObj;


    // Start is called before the first frame update
    void Start()
    {
        //Find the player and ground objects by tag. This should be ok, as this is done only once in the beginning of the game
        groundObj = GameObject.FindGameObjectWithTag("ground");
        //Spawn an initial cheese
        spawnCheese();

        playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    //Spawn cheese function
    public void spawnCheese()
    {
        //If there is a cheese still in the game, destroy it. This should be not necessary, but we have it as a precaution
        if (cheeseObj != null)
        {
            Destroy(cheeseObj);
        }
        //Get half the ground plane size minus the padding - this will be used to specify a random place the cheese is spawned
        float halfGroundSize = (groundObj.transform.localScale.x / 2f) - padding;
        Vector3 randomSpawnPos = new Vector3(Random.Range(-halfGroundSize, halfGroundSize), 2, Random.Range(-halfGroundSize, halfGroundSize));
        //Instantiate a new cheese object at the specified random position
        cheeseObj = Instantiate(spawnableObj, randomSpawnPos, Random.rotation);
    }

    // Spawn enemy function
    public void spawnEnemy( float speed)
    {
        //Choose at random an enemy spawner to use 
        int randomIndex = Random.Range(0, enemySpawners.Length);

        Transform enemySpawner = enemySpawners[randomIndex].transform;

        //Get the enemy spawner half size in x and z direction 
        float halfX = (enemySpawner.localScale.x / 2f);
        float halfZ = (enemySpawner.localScale.z / 2f);

        
        //Use the spawner half sizes and position to generate a random enemy spawn position
        Vector3 randomSpawnPos = new Vector3(Random.Range(enemySpawner.position.x - halfX, enemySpawner.position.x + halfX), 1,
                                            Random.Range(enemySpawner.position.z - halfZ, enemySpawner.position.z + halfZ));

        //Instantiate an enemy object
        GameObject enemyObj = Instantiate(spawnableEnemy, randomSpawnPos, Quaternion.identity);

        //Set its speed to the current speed value
        enemyObj.GetComponent<EnemyBehaviour>().speed = speed;
        //Set the rotation of the enemy so it is looking at the player
        enemyObj.transform.LookAt(playerObj.transform);

    }

    //Basic timer structure. We also update the enemy speed and enemy spawn timer every iteration together with spawning a new enemy. This was the game becomes harder
    void Update()
    {

        elapsedTime += Time.deltaTime;

        if (elapsedTime > timerEnemy)
        {
            elapsedTime = 0;

            spawnEnemy(enemySpeed);

            if (enemySpeed < 25)
            {
                enemySpeed += 0.2f;
            }
            

            if (timerEnemy>0.5f)
            {
                timerEnemy -= 0.2f;
            }
            

            Debug.Log("Spawned!");
        }

    }

}
