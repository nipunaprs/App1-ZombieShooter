using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xRotation = 0f; //Gives a clamp to up and down so Y value doesn't go crazy

    // Start is called before the first frame update
    void Start()
    {
        //Ensures mouse doesn't leave the game window
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Getting mouse input from input manager, multiplying by sensistivity and then making sure its not linked to framerate.
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); //clamp max and min viewing angles by 90

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); //Provides vector with x, y, z
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
