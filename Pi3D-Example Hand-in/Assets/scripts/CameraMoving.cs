using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for camera movement - both using a mouse and a keyboard
/// </summary>
public class CameraMoving : MonoBehaviour
{
    //The pivot Transform used to anchor the camera and provide correct translation and rotation
    private Transform pivot;
    //Local rotation Vector3 containing the current pivot rotation and taking the mouse input
    private Vector3 localRotation;
    //Distance between the pivot and the camera
    private float cameraDistance = 10f;

    //All the serialized fields that can be changed from the Inspector
    // Used to change the mouse input feeling by changed the sensitivity, scroll speed for zoom, and dampening for smoother transition
    [SerializeField]
    private float mouseSensitivity = 4f;
    [SerializeField]
    private float scrollSensitvity = 2f;
    [SerializeField]
    private float orbitDampening = 10f;
    [SerializeField]
    private float scrollDampening = 6f;

    //When the game starts this is the initial rotation of the camera pivot, this is used so there is no initial ugly snapping of the camera
    [SerializeField]
    private Vector3 pivotStartingRotation;
    //Vector2 containing the min and max distances that the camera zoom is clamped at so it does not go in the ground or too far away
    [SerializeField]
    private Vector2 distanceClamp;
    //Clamp of the Y axis rotation
    [SerializeField]
    private Vector2 yClamp;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize the pivot, set the starting rotation and get the local rotation
        pivot = transform.parent;

        pivot.rotation = Quaternion.Euler(pivotStartingRotation);
        localRotation = new Vector3(pivotStartingRotation.y, pivotStartingRotation.x, pivotStartingRotation.z);

    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Function for updating the camera rotation - we use LateUpdate to minimize camera jitter
        RotateCamera();
    }

    //Camera rotation function
    void RotateCamera()
    {
        //Check if right mouse button is pressed - if it is hide the cursor and get mouse posiiton for rotation
        if (Input.GetMouseButton(1))
        {

            HideAndLockCursor();
            getMouseInputs();

        }
        else
        {
            //Show the mosue curson when button is let go
            ShowAndUnlockCursor();
        }

    }

    // Function for getting the mouse inputs 
    void getMouseInputs()
    {
        //Check if the mouse has been moved, no need to move it if it's idle
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            //Get the X and Y mouse movement and modify it with the chosen sensitivity
            localRotation.x += Input.GetAxis("Mouse X") * mouseSensitivity;
            localRotation.y -= Input.GetAxis("Mouse Y") * mouseSensitivity;

            //Clamp the y Rotation to horizon and not flipping over at the top
            localRotation.y = Mathf.Clamp(localRotation.y, yClamp.x, yClamp.y);

        }
        //Zooming Input from our Mouse Scroll Wheel
        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            //Get the scroll wheel input change and modify with sensitivity
            float scrollAmount = Input.GetAxis("Mouse ScrollWheel") * scrollSensitvity;
            //update the scrool amount and add a bit of dampening to the distance
            scrollAmount *= (cameraDistance * 0.3f);
            //Inverse the camera distance scroll so the camera moves back when we scroll back and vice versa
            cameraDistance += scrollAmount * -1f;
            //Clamp the distance to the maximum values
            cameraDistance = Mathf.Clamp(cameraDistance, distanceClamp.x, distanceClamp.y);
        }
        //Create a quaternion of the local  rotation - we only care for yaw and pitch, but no roll, so it is zeroed out. We use the mouse Y for X rotation and mouse X for Y rotation
        //as the coordinates in the pivot are rotated around the specific 3D axis, while the mouse gives us movement in a 2D plane
        Quaternion localQuaternion = Quaternion.Euler(localRotation.y, localRotation.x, 0);
        //We use the Lerp function to go from the previous rotation to the new rotation using the dampening and Time.deltaTime for a smoother rotation and translation
        pivot.rotation = Quaternion.Lerp(pivot.rotation, localQuaternion, Time.deltaTime * orbitDampening);
        transform.localPosition = new Vector3(0f, 0f, Mathf.Lerp(transform.localPosition.z, cameraDistance * -1f, Time.deltaTime * scrollDampening));
    }


    //Two functions to lock and uplock cursor and change its visibility
    void ShowAndUnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void HideAndLockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


}
