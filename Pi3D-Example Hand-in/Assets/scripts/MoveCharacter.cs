using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for character movement controls. Short script that is made much more complicated as the player's movement is influenced by the camera rotation
/// The movement is based on Rigidbody velocity so the collisions are more stable
/// </summary>
public class MoveCharacter : MonoBehaviour
{
    //Speed of the player
    [SerializeField]
    private float movementSpeed = 2;

    //Rigidbody of the player object
    private Rigidbody currRigidBody;
    //Vector3 containing the input from the player
    private Vector3 playerInput;

    //Camera pivot object. Again we have one camera so we can set it up as a singleton or a serialized object
    [SerializeField]
    private GameObject cameraPivot;

    // Start is called before the first frame update
    void Start()
    {
        currRigidBody = transform.GetComponent<Rigidbody>();
    }

    // We are using rigidbody motion so FixedUpdate is necessary to ensure the physics are not connected to the framerate
    void FixedUpdate()
    {
        if (ScoreKeeper.livesScore > 0)
        {
            moveCharacter();
        }
        else
        {
            currRigidBody.velocity = Vector3.zero;
        }
        


        //// Old way to do the movement using direct changes with the WASD keys - this works good as long as the camera does not rotate
        //if (Input.GetKey(KeyCode.W))
        //{
        //    //transform.Translate(transform.forward * Time.deltaTime * movementSpeed, Space.World);
        //    currRigidBody.velocity = transform.forward * movementSpeed * Time.fixedDeltaTime;
        //}

        //if (Input.GetKey(KeyCode.S))
        //{
        //    //transform.Translate(-transform.forward * Time.deltaTime * movementSpeed, Space.World);
        //    currRigidBody.velocity = -transform.forward * movementSpeed * Time.fixedDeltaTime;
        //}

        //if (Input.GetKey(KeyCode.A))
        //{
        //    transform.Rotate(transform.up, -rotationSpeed * Time.fixedDeltaTime);
        //}

        //if (Input.GetKey(KeyCode.D))
        //{
        //    transform.Rotate(transform.up, rotationSpeed * Time.fixedDeltaTime);
        //}

    }

    void moveCharacter()
    {
        //Get the axis of the horizontal and vertical movement. These are predefined in unity to use either the WASD or arrow controls. This can be change in the Options
        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        //Get the right and forward direction of the camera pivot
        Vector3 right = cameraPivot.transform.right;
        Vector3 forward = cameraPivot.transform.forward;

        //Zero out the y direction of the camera pivot. We don't care for the way direction, but just the yaw - aka the Z direciton
        forward.y = 0f;
        right.y = 0f;
        //Normalize the whole thing just in case
        forward.Normalize();
        right.Normalize();

        // Multiply the horizontal axis movement with the right direction and the vertical axis movement with the forward. 
        //Essentially rotate the movement directions based on the forward and right directions. 
        Vector3 combined = (right * horiz + forward * vert) * movementSpeed;

        //Setup the movement Vector3 using the rotated movements in X and Z and a constant y velocity coming form the gravity
        playerInput = new Vector3(combined.x, currRigidBody.velocity.y, combined.z);
        //playerInput = new Vector3(Input.GetAxis("Horizontal") * movementSpeed, currRigidBody.velocity.y, Input.GetAxis("Vertical") * movementSpeed);

        //If we have pressed a button rotate the based on the inputs
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {

            transform.LookAt(transform.position + playerInput);
        }

        //Give the new input as a velocity vector.
        currRigidBody.velocity = playerInput;
    }

}
