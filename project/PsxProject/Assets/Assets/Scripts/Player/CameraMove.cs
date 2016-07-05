using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
    //this should be added to the main object, must have a Camera as a child


    private ControllerManager currentControllerManager;
    private Camera currentCamera;

    // Use this for initialization
    void Start () {
        currentControllerManager = GetComponent<ControllerManager>();
        currentCamera = Transform.FindObjectOfType<Camera>();
        Cursor.visible = false; //hides mouse
    }
	
	// Update is called once per frame
	void Update () {
        currentCamera.transform.Rotate(new Vector3(0, Input.GetAxis(currentControllerManager.lookX) * currentControllerManager.sensibility, 0), Space.World);
        currentCamera.transform.Rotate(new Vector3(-Input.GetAxis(currentControllerManager.lookY) * currentControllerManager.sensibility, 0, 0));
    }
}
