using UnityEngine;
using System.Collections;

public enum EntityActionStates //used to control animations and other states
{
    IDLE, WALK, RUN, JUMP, ATTACK, CROUCH, INTERACT
}

public enum EntityMindStates //used to control AI. Player means entitiy is player controlled
{
    PLAYER, IDLE, SEARCH, HOSTILE, HURT, DEAD, NEUTRAL, FAKING
}


public class BaseEntity : MonoBehaviour {
    public int health;
    [HideInInspector]
    public EntityActionStates actionState;
    [HideInInspector]
    public EntityMindStates mindState;

    void Start()
    {
        actionState = EntityActionStates.IDLE;
        mindState = EntityMindStates.IDLE;
    }
	
}
