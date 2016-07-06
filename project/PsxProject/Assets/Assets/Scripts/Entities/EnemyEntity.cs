using UnityEngine;
using System.Collections;



public class EnemyEntity : BaseEntity
{
    public enum FoundTargetType //used to control mind state
    {
        VISIBLE_TARGET, HEARABLE_SOUND, VISIBLE_MEMORY, HEARABLE_MEMORY, NOTHING
    }

    public Rigidbody currentRigidBody;
    // Use this for initialization

    //enemy entity will have all of its knowledge
    private FoundTargetType lastFoundTarget;
    //for search behavior
    private Vector3 lastHeardSoundPosition;
    private Vector3 lastSeenPosition;

    //for hunt/run/attack behavior
    private Vector3 targetPosition;
    private Vector3 targetSound;

    //

    void Start ()
    {
        currentRigidBody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //calculates the next mental state

        //if not hurt or faking or dead
        if(mindState != EntityMindStates.FAKING && mindState != EntityMindStates.DEAD)
        {
            CheckSurroundings();
        }
        switch (lastFoundTarget)
        {
            case FoundTargetType.VISIBLE_TARGET:
            case FoundTargetType.VISIBLE_MEMORY:
                // sets targetPosition as target position;
                //if neutral/idle/search, enter HOSTILE
                //else enters HURT
                break;
            case FoundTargetType.HEARABLE_SOUND:
            case FoundTargetType.HEARABLE_MEMORY:
                //sets targetSound as sound position
                //if neutral/idle/search, enter SEARCH
                break;
        }

        switch (mindState)
        {
            case EntityMindStates.NEUTRAL:
                //walks around. (Sets a waypoint, moves toward it, finds another waypoint)
                break;
            case EntityMindStates.SEARCH:
                //walks toward last seen position or last sound position if has not seen the target
                break;
            case EntityMindStates.HOSTILE:
                //runs toward visible target or last seen position, attacks if within range
                break;
            case EntityMindStates.HURT:
                //runs away from hostile target, searches for innofensive target for food / health
                break;
            case EntityMindStates.FAKING:
                //stay on dead state until target gets within reach. attacks player when within range
                break;
            case EntityMindStates.DEAD:
                //stays on ground. might be used as food for other kinds of enemies
                break;
        }
	}

    void CheckSurroundings ()
    {
        //is there a player or target in sight?
        //if not
        //is there a sound?
        //this sets the content of lastFoundTarget
    }
}
