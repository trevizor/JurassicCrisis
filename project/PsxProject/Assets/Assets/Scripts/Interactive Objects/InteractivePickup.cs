using UnityEngine;
using System.Collections;

public class InteractivePickup : InteractiveObject
{

    // Use this for initialization

    private bool isEmpty = false;
    public Item currentItem;

    void Start()
    {
        currentItem = new Key("ABC123", "Key");
    }

    public override void Interact()
    {
        Debug.Log("Entered Interact of pickup");
        if (!isEmpty)
        {
            GetPickup ();
        }
    }

    public void GetPickup()
    { //this will return an Item
        isEmpty = true;
        Debug.Log("User got pickup: " + currentItem);
        GameObject.Destroy(gameObject);
    }

    public void SetContents(Item _targeContent)
    { //this will pass an Item

    }
}
