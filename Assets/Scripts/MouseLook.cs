using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivityX = 100f;
    public float mouseSensitivityY = 100f;
    public Transform horizontal;
    public Transform vertical;

    float xRotation = 0f;
    float yRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivityX;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivityY;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        yRotation += mouseX;
        yRotation %= 360f;
        horizontal.localRotation = Quaternion.Euler(0f, yRotation, 0f);
        vertical.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //transform.localRotation = Quaternion.Euler(xRotation, yRotation, 0f);
    }
}
