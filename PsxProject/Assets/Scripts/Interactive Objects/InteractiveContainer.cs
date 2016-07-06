using UnityEngine;
using System.Collections;

public class InteractiveContainer : InteractiveObject
{

	// Use this for initialization

	private bool isEmpty = false;
	public Item currentItem;

	void Start ()
    {
        //debug code
        currentItem = new Key("ABC123", "Key");
        
	}

	public override void Interact ()
    {
		if(!isEmpty){
			GetContents();
		} else {
			Debug.Log("Container is empty.");
		}
	}

    public override void SetTargetActor(BaseEntity _targetActor)
    {
        targetActor = _targetActor;
    }

    public void GetContents () //this will return an Item
    { 
		isEmpty = true;
        Debug.Log(targetActor);
        if (targetActor is PlayerEntity)
        {
            PlayerEntity targetPlayer = targetActor as PlayerEntity;
            
            targetPlayer.AddItemToInventory(currentItem);
            if (currentItem is Key) //this is an example of how to use the item as it really is (key, weapon, health, etc) //DEBUG CODE
            {
                Key tempKey = currentItem as Key;
                Debug.Log("User got key: " + tempKey.fingerprint);
            } else
            {
                Debug.Log("User got item from container: " + currentItem);
            }
        }
        
	}

	public void SetContents (Item _targeContent)  //this will pass an Item
    {
    
    }
}
