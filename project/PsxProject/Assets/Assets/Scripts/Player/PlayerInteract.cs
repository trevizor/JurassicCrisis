using UnityEngine;
using System.Collections;

public class PlayerInteract : MonoBehaviour {
	private ControllerManager currentControllerManager;
	private Camera currentCamera;
	private Rigidbody currentRigidBody;
    private PlayerEntity currentPlayerEntity;

    private const float INTERACTION_RANGE = 3.0f;

	// Use this for initialization
	void Start () {
        currentPlayerEntity = GetComponent<PlayerEntity>();
        currentRigidBody = currentPlayerEntity.currentRigidBody;
        currentControllerManager = currentPlayerEntity.currentControllerManager;
        currentCamera = currentPlayerEntity.currentCamera;
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown(currentControllerManager.use))
        {
        	RaycastHit targetHit;
            Debug.DrawRay(currentCamera.transform.position, currentCamera.transform.forward, Color.green, 1f);
            if ( Physics.Raycast(currentCamera.transform.position, currentCamera.transform.forward, out targetHit, INTERACTION_RANGE) ){
            	InteractiveObject target = targetHit.transform.gameObject.GetComponent<InteractiveObject>();
                if(target is InteractiveObject)
                {
                    target.SetTargetActor(currentPlayerEntity);
                    target.Interact();
                }
            }
        }
	}
}
