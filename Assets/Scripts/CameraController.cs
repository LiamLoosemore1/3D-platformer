using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform target;

    public Vector3 offset;

    public bool useOffsetValues;

    public float rotateSpeed;

	void Start () 
	{
        if (!useOffsetValues)
        {
            offset = target.position - transform.position;
        }
	}
	
	// Update is called once per frame
	void Update () 
	{
        // get the x position of the mouse and rotate the traget
        float horizontal = Input.GetAxis("MouseX") * rotateSpeed;
        target.Rotate(0, )

        transform.position = target.position - offset;

		transform.LookAt(target);
	}
}
