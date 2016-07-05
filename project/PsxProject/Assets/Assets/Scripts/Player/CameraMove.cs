using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
    //this should be added to the main object, must have a Camera as a child

        //used to clamp rotation and prevent breaking your neck.
    private float minimumX = -360F;
    private float maximumX = 360F;
    private float minimumY = -70F;
    private float maximumY = 70F;
    private float rotationX = 0F;
    private float rotationY = 0F;
    Quaternion originalRotation;


    //to create head bob effect    
    public float transitionSpeed = 200f; //smooths out the transition from moving to not moving.
    public float bobSpeed = 3.5f; //how quickly the player's head bobs.
    public float bobAmount = 0.06f; //how dramatic the bob is. Increasing this in conjunction with bobSpeed gives a nice effect for sprinting.

    float timer = Mathf.PI / 2; //initialized as this value because this is where sin = 1. So, this will make the camera always start at the crest of the sin wave, simulating someone picking up their foot and starting to walk--you experience a bob upwards when you start walking as your foot pushes off the ground, the left and right bobs come as you walk.

    private ControllerManager currentControllerManager;
    private Camera currentCamera;

    private Vector3 restPosition; //local position where your camera would rest when it's not bobbing.
    private Vector3 standingRestPosition;
    private Vector3 crouchRestPosition;


    // Use this for initialization
    void Start () {
        currentControllerManager = GetComponent<ControllerManager>();
        currentCamera = Transform.FindObjectOfType<Camera>();
        originalRotation = currentCamera.transform.localRotation;
        restPosition = currentCamera.transform.localPosition;
        crouchRestPosition = new Vector3(currentCamera.transform.localPosition.x, currentCamera.transform.localPosition.y*0.5f, currentCamera.transform.localPosition.z); //sets crouch height to 65% of normal height
        standingRestPosition = restPosition;

        Cursor.visible = false; //hides mouse
    }
	
	// Update is called once per frame
	void Update () {
        rotationX += Input.GetAxis(currentControllerManager.lookX) * currentControllerManager.sensibility * Time.smoothDeltaTime;
        rotationY += Input.GetAxis(currentControllerManager.lookY) * currentControllerManager.sensibility * Time.smoothDeltaTime;
        //used to prevent full Y rotation
        rotationX = ClampAngle(rotationX, minimumX, maximumX);
        rotationY = ClampAngle(rotationY, minimumY, maximumY);
        Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.up);
        Quaternion yQuaternion = Quaternion.AngleAxis(rotationY, -Vector3.right);
        currentCamera.transform.localRotation = originalRotation * xQuaternion * yQuaternion;
        //calculate head bob. This could be somewhere better? could.
        CalculateHeadBob();
    }
    void CalculateHeadBob ()
    {
        //we can set the head bob based on the rigid body velocity!
        //set camera height based on crouch input
        restPosition = standingRestPosition;
        if (Input.GetButton(currentControllerManager.crouch))
        {
            restPosition = crouchRestPosition;
        }

        if (Input.GetAxisRaw(currentControllerManager.walkX) != 0 || Input.GetAxisRaw(currentControllerManager.walkY) != 0) //TODO: use player's state machine would be a better way to do this
        {
            timer += bobSpeed * Time.deltaTime;

            //use the timer value to set the position
            Vector3 newPosition = new Vector3(Mathf.Cos(timer) * bobAmount, restPosition.y + Mathf.Abs((Mathf.Sin(timer) * bobAmount)), restPosition.z); //abs val of y for a parabolic path
            currentCamera.transform.localPosition = newPosition;
        }
        else
        {
            timer = Mathf.PI / 2; //reinitialize

            Vector3 newPosition = new Vector3(Mathf.Lerp(currentCamera.transform.localPosition.x, restPosition.x, transitionSpeed * Time.deltaTime), Mathf.Lerp(currentCamera.transform.localPosition.y, restPosition.y, transitionSpeed * Time.deltaTime), Mathf.Lerp(currentCamera.transform.localPosition.z, restPosition.z, transitionSpeed * Time.deltaTime)); //transition smoothly from walking to stopping.
            currentCamera.transform.localPosition = newPosition;
        }

        if (timer > Mathf.PI * 2) //completed a full cycle on the unit circle. Reset to 0 to avoid bloated values.
            timer = 0;
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360f)
            angle += 360f;
        if (angle > 360f)
            angle -= 360f;
        return Mathf.Clamp(angle, min, max);
    }
}
