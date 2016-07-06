using UnityEngine;
using System.Collections;

public class InteractiveContainer : InteractiveObject
{

	// Use this for initialization

	private bool isEmpty = false;
	public Item currentItem;

	void Start () {

        currentItem = new Key("ABC123", "Key");

        //currentItem = new WeaponItem();
        Debug.Log(currentItem);
	}

	public override void Interact () {
		if(!isEmpty){
			GetContents();
		} else {
			Debug.Log("Container is empty.");
		}
	}

	public void GetContents () { //this will return an Item
		isEmpty = true;
        if(currentItem is Key ){
            Key tempKey = currentItem as Key;
            Debug.Log("User got contents: " + tempKey.fingerprint);
        } else
        {
            Debug.Log("fuck it");
        }
	}

	public void SetContents (Item _targeContent) { //this will pass an Item
    
    }
}
