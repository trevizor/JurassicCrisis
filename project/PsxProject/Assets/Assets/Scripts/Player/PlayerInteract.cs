using UnityEngine;
using System.Collections;

public class PlayerInteract : MonoBehaviour {
	private ControllerManager currentControllerManager;
	private Camera currentCamera;
	private Rigidbody currentRigidBody;

	// Use this for initialization
	void Start () {
		currentRigidBody = GetComponent<Rigidbody>();
		currentControllerManager = GetComponent<ControllerManager>();
        currentCamera = Transform.FindObjectOfType<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown(currentControllerManager.use))
        {
        	RaycastHit hit;
            if( Physics.Raycast(currentRigidBody.position, currentCamera.transform.forward, out hit, 2f) ){
            	InteractiveObject target = hit.transform.gameObject.GetComponent<InteractiveObject>();
            	target.Interact();
            }
        }
	}
}
