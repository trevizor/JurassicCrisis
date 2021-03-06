﻿using UnityEngine;
using System.Collections;

public enum PlayerIdentifier {
    P1, P2, P3, P4
}

public class ControllerManager : MonoBehaviour {


    public PlayerIdentifier assignedPlayer = PlayerIdentifier.P1;
    public float sensibility = 2.0f;
    [HideInInspector] public string walkX;
    [HideInInspector] public string walkY;
    [HideInInspector] public string lookX;
    [HideInInspector] public string lookY;
    [HideInInspector] public string jump;
    [HideInInspector] public string fire;
    [HideInInspector] public string use;
    [HideInInspector] public string crouch;
    [HideInInspector] public string run;
    


    // Use this for initialization
    void Start () {
        //TODO: Set strings for the desired player based on P1, P2 etc
        walkX = assignedPlayer + " Horizontal";
        walkY = assignedPlayer + " Vertical";
        jump = assignedPlayer + " Jump";
        fire = assignedPlayer + " Fire";
        use = assignedPlayer + " Use";
        lookX = assignedPlayer + " Look X";
        lookY = assignedPlayer + " Look Y";
        crouch = assignedPlayer + " Crouch";
        run = assignedPlayer + " Run";
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
