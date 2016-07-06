using UnityEngine;
using System.Collections;

public abstract class InteractiveObject : MonoBehaviour {
    public BaseEntity targetActor;
	public abstract void Interact ();
    public abstract void SetTargetActor(BaseEntity _targetActor);
}
