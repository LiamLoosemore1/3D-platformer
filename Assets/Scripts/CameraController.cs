using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;

    public Vector3 offset;

    public bool useOffsetValues;

    public float rotateSpeed;

    public Transform pivot;

    public float maxViewAngle;
    public float minViewAngle;

    public bool invertY;

    void Start()
    {
        if (!useOffsetValues)
        {
            offset = target.position - transform.position;
        }

        pivot.transform.position = target.transform.position;
        //pivot.transform.parent = target.transform;
        pivot.transform.parent = null;

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        pivot.transform.position = target.transform.position;

        //Get the X position of the mouse and rotate the target
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        pivot.Rotate(0, horizontal, 0);

        //Get the Y position of the mouse & rotate the pivot
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        if (invertY)
        {
            pivot.Rotate(vertical, 0, 0);
        }
        else
        {
            pivot.Rotate(-vertical, 0, 0);
        }

        //limit up/down camera rotation
        if (pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 90f)
        {
            pivot.rotation = Quaternion.Euler(maxViewAngle, pivot.rotation.eulerAngles.y, pivot.rotation.eulerAngles.z);
        }


        //var test = 360f + minViewAngle;
        //if (pivot.rotation.eulerAngles.x < test)
        //{
        //    var x = 5;
        //}

        if (pivot.rotation.eulerAngles.x < 360f + minViewAngle && pivot.rotation.eulerAngles.x > 90f)
        {
            pivot.rotation = Quaternion.Euler(360f + minViewAngle, pivot.rotation.eulerAngles.y, pivot.rotation.eulerAngles.z);
        }

        //Move the camera based on the current rotation of the target & the oraginal offset
        float desiredYAngle = pivot.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rotation * offset);

        //transform.position = target.position - offset;

        if (transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y - .5f, target.position.z);
        }

        transform.LookAt(target);
    }
}
