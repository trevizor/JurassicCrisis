using UnityEngine;
using System.Collections;

public class PlayerEntity : BaseEntity
{
    public PlayerInventory playerInventory;
    public ControllerManager currentControllerManager;
    public Camera currentCamera;
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
}
