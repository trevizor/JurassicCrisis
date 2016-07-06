using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{

    private List<Item> currentInventory;

	// Use this for initialization
	void Start ()
    {
        currentInventory = new List<Item>();
	}
	
    public void AddItemToInventory (Item _itemToAdd)
    {
        currentInventory.Add(_itemToAdd);
    }

	//counts how many items the user has on inventory
	public int Count ()
    { 
        return currentInventory.Count;
    }
}
