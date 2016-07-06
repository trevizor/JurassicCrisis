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
    private float actorStandHeight;
    private bool isGrounded = false;
    private bool isColliding = false;

    private PlayerEntity currentPlayerEntity;
    private Rigidbody currentRigidBody;
    private ControllerManager currentControllerManager;
    private Camera currentCamera;

    private const float HEIGHT_MODIFIER = 0.05f;

    void Start()
    {
        //sets the component references
        currentPlayerEntity = GetComponent<PlayerEntity>();
        currentRigidBody = currentPlayerEntity.currentRigidBody;
        currentControllerManager = currentPlayerEntity.currentControllerManager;
        currentCamera = currentPlayerEntity.currentCamera;
        
        //whatever the rigid body needs
        currentRigidBody.freezeRotation = true;
        currentRigidBody.useGravity = false;

        //sets the actor stand height based on the collider size
        actorStandHeight = GetComponent<CapsuleCollider>().height + HEIGHT_MODIFIER; //adds HEIGHT_MODIFIER so it works better with slopes //TODO: find a better way to check isGrounded

        //sets movement variables
        walkSpeed = 3.0f;
        runSpeed = 5.0f;
        crouchSpeed = 1.5f;
        gravityForce = 12.0f;
        maxVelocityChange = 2.0f;
        jumpHeight = 0.95f;

    }

    void FixedUpdate()
    {
        bool isRunPressed = Input.GetButton(currentControllerManager.run);
        bool isCrouchPressed = Input.GetButton(currentControllerManager.crouch);
        //set how fast the player will move
        float movementSpeed = walkSpeed;
        if (isRunPressed)
        {
            movementSpeed = runSpeed;
        }
        if (isCrouchPressed)
        {
            movementSpeed = crouchSpeed;
        }

        // Calculate how fast we should be moving
        Vector3 targetVelocity = new Vector3(Input.GetAxis(currentControllerManager.walkX), 0, Input.GetAxis(currentControllerManager.walkY)).normalized;
        targetVelocity = currentCamera.transform.TransformDirection(targetVelocity);
        targetVelocity *= movementSpeed;

        // Apply a force that attempts to reach our target velocity
        Vector3 velocity = currentRigidBody.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;

        //only moves if is on ground or not colliding with a wall
        if( (!isColliding || isGrounded))
        {
            currentRigidBody.AddForce(velocityChange, ForceMode.VelocityChange);
        }

        // Jump - only jumps if isGrounded
        if (Input.GetButton(currentControllerManager.jump) && isGrounded)
        {
            currentRigidBody.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
        }

        //updates isGrounded status
        isGrounded = CheckGrounded();

        // We apply gravity manually for more tuning control
        if (!isGrounded)
        {
            currentRigidBody.AddForce(new Vector3(0, -gravityForce * currentRigidBody.mass, 0));
        }



        //movement state control
        if (targetVelocity.magnitude > 0)
        {
            if (!isRunPressed && !isCrouchPressed)
            {
                currentPlayerEntity.SetStateWalking();
            }
            else
            {
                if (isRunPressed)
                {
                    currentPlayerEntity.SetStateRunning();
                }
                else
                {
                    currentPlayerEntity.SetStateCrouching();
                }
            }
        }
        else
        {
            currentPlayerEntity.SetStateIdle();
        }

        //resets isColliding
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