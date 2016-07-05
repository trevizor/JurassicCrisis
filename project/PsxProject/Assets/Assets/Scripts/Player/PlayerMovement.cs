using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]

public class PlayerMovement : MonoBehaviour
{

    private float runSpeed;
    private float walkSpeed;
    private float crouchSpeed;
    private float gravityForce;
    private float maxVelocityChange;
    private float jumpHeight;
    private float airControl;
    private float actorStandHeight;
    private float actorCrouchHeight;
    private bool grounded = false;
    private bool isColliding = false;

    private Rigidbody currentRigidBody;
    private ControllerManager currentControllerManager;
    private Camera currentCamera;
    

    void Awake()
    {
        currentRigidBody = GetComponent<Rigidbody>();
        currentControllerManager = GetComponent<ControllerManager>();
        currentCamera = Transform.FindObjectOfType<Camera>();
        Debug.Log(currentControllerManager);
        currentRigidBody.freezeRotation = true;
        currentRigidBody.useGravity = false;
        actorStandHeight = GetComponent<CapsuleCollider>().height + 0.1f; //adds 0.1f so it works better with slopes //TODO: find a better way to check grounded

        walkSpeed = 3.0f;
        runSpeed = 5.0f;
        crouchSpeed = 1.5f;
        gravityForce = 12.0f;
        maxVelocityChange = 2.0f;
        jumpHeight = 0.95f;
        airControl = 0.4f;

    }

    void FixedUpdate()
    {
        //set how fast the player will move
        float movementSpeed = walkSpeed;
        if (Input.GetButton(currentControllerManager.run))
        {
            movementSpeed = runSpeed;
        }
        if (Input.GetButton(currentControllerManager.crouch))
        {
            movementSpeed = crouchSpeed;
        }

        // Calculate how fast we should be moving
        Vector3 targetVelocity = new Vector3(Input.GetAxis(currentControllerManager.walkX), 0, Input.GetAxis(currentControllerManager.walkY));
        targetVelocity = currentCamera.transform.TransformDirection(targetVelocity);
        targetVelocity *= movementSpeed;

        // Apply a force that attempts to reach our target velocity
        Vector3 velocity = currentRigidBody.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;

        if( (!isColliding || grounded))
        {
            currentRigidBody.AddForce(velocityChange, ForceMode.VelocityChange);
        }

        // Jump
        if (Input.GetButton(currentControllerManager.jump) && grounded)
        {
            currentRigidBody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
        }

        //updates grounded status
        grounded = CheckGrounded();

        // We apply gravity manually for more tuning control
        if (!grounded)
        {
            currentRigidBody.AddForce(new Vector3(0, -gravityForce * currentRigidBody.mass, 0));
        }
        isColliding = false;
    }

    bool CheckGrounded ()
    {
        bool isGrounded = false;
        float varDiff = 0.24f;
        Vector3 forwardPos = new Vector3(currentRigidBody.transform.position.x + varDiff, currentRigidBody.transform.position.y, currentRigidBody.transform.position.z);
        Vector3 backwardPos = new Vector3(currentRigidBody.transform.position.x - varDiff, currentRigidBody.transform.position.y, currentRigidBody.transform.position.z);
        Vector3 leftPos = new Vector3(currentRigidBody.transform.position.x, currentRigidBody.transform.position.y, currentRigidBody.transform.position.z + varDiff);
        Vector3 rightPos = new Vector3(currentRigidBody.transform.position.x, currentRigidBody.transform.position.y, currentRigidBody.transform.position.z - varDiff);
        //verify 4 points of contact
        isGrounded = (Physics.Raycast(forwardPos, Vector3.down, actorStandHeight) ||
            Physics.Raycast(backwardPos, Vector3.down, actorStandHeight) ||
            Physics.Raycast(leftPos, Vector3.down, actorStandHeight) ||
            Physics.Raycast(rightPos, Vector3.down, actorStandHeight)
            );

        return isGrounded;
    }

    void OnCollisionStay()
    {
        isColliding = true;
    }

    float CalculateJumpVerticalSpeed()
    {
        // From the jump height and gravity we deduce the upwards speed 
        // for the character to reach at the apex.
        return Mathf.Sqrt(2 * jumpHeight * gravityForce);
    }
}