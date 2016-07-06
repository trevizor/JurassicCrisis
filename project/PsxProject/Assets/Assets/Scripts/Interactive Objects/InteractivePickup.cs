using UnityEngine;
using System.Collections;

public class InteractivePickup : InteractiveObject
{

    // Use this for initialization
    private bool isEmpty = false;
    public Item currentItem;

    void Start()
    {
        //debug code
        currentItem = new Key("ASD456", "Key");
    }

    public override void Interact()
    {
        if (!isEmpty)
        {
            GetPickup ();
        }
    }

    public override void SetTargetActor(BaseEntity _targetActor)
    {
        targetActor = _targetActor;
    }

    public void GetPickup()
    { //this will return an Item
        isEmpty = true;
        Debug.Log(targetActor);
        if(targetActor is PlayerEntity)
        {
            PlayerEntity targetPlayer = targetActor as PlayerEntity;
            Debug.Log("User got pickup: " + currentItem);
            targetPlayer.AddItemToInventory(currentItem);
            GameObject.Destroy(gameObject);
        }
        
    }

    public void SetContents(Item _targeContent)
    { //this will pass an Item

    }
}
