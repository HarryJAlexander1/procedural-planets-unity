using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour
{
    public float moveSpeed;
    public float boostedMoveSpeed;
    public float mouseSensitivity;
    private float xRotation = 0.0f;
    private float yRotation = 0.0f;
    private bool cursorVisible;

    void Start()
    {
        ToggleCursorVisibility();
    }

    void Update()
    {
        EnableCameraControl();
    }

    private void EnableCameraControl() {

        // Get input from WASD keys
        float horizontal = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float vertical = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        // Check for Shift key being held down
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            horizontal *= (boostedMoveSpeed / moveSpeed);
            vertical *= (boostedMoveSpeed / moveSpeed);
        }

        // Move the camera
        transform.Translate(new Vector3(horizontal, 0, vertical));

        // Get mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Calculate and clamp camera rotation
        xRotation -= mouseY;
        yRotation += mouseX;
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

        // Apply camera rotation
        transform.eulerAngles = new Vector3(xRotation, yRotation, 0);
    }

    private void ToggleCursorVisibility()
    {
        cursorVisible = false;
        Cursor.visible = cursorVisible;
        Cursor.lockState = cursorVisible ? CursorLockMode.None : CursorLockMode.Locked;
    }
}