using UnityEngine;
using System.Collections;

public class PlayerEntity : BaseEntity
{
    [HideInInspector]
    public PlayerInventory playerInventory;
    [HideInInspector]
    public ControllerManager currentControllerManager;
    [HideInInspector]
    public Camera currentCamera;
    [HideInInspector]
    public Rigidbody currentRigidBody;
    // Use this for initialization
    void Start ()
    {
        //finds and sets values that will be used by the player's controllers
        currentRigidBody = GetComponent<Rigidbody>();
        currentControllerManager = GetComponent<ControllerManager>();
        currentCamera = Transform.FindObjectOfType<Camera>();
        playerInventory = GetComponent<PlayerInventory>();
        mindState = EntityMindStates.PLAYER;
    }

    public void AddItemToInventory (Item _targetItem) //adds an item to the player inventory
    {
        playerInventory.AddItemToInventory(_targetItem);
        Debug.Log(currentControllerManager.assignedPlayer + " has " + playerInventory.Count() + " Items on his inventory.");
    }
    


    //State controller
    public void SetStateRunning()
    {
        actionState = EntityActionStates.RUN;
    }

    public void SetStateWalking()
    {
        actionState = EntityActionStates.WALK;
    }

    public void SetStateCrouching()
    {
        actionState = EntityActionStates.CROUCH;
    }

    public void SetStateIdle()
    {
        actionState = EntityActionStates.IDLE;
    }

    public void SetStateJumping()
    {
        actionState = EntityActionStates.JUMP;
    }

    
}
