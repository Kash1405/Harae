using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	public float mouseSensitivity = 100f;
	public Transform playerBody;
	public Transform model;
	
	float xRotation = 0f;
	
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
		transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
		
		playerBody.Rotate(Vector3.up * mouseX);
		
		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -90f, 90f);
		transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
		
		//float x = model.position.x + 8f, y = model.position.y + 15f, z = model.position.z - 10f;
		//if(x-transform.position.x < 5 || )
		//transform.position = new Vector3(, , );
    }
}
