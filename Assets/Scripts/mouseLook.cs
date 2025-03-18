using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody; // Oyuncu karakteri (Ana obje)

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Kamerayı sadece yukarı-aşağı döndür (X ekseninde)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Bakış açısını -90 ile 90 derece arasında sınırla
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Karakterin sadece Y ekseninde dönmesini sağla
        playerBody.Rotate(Vector3.up * mouseX);
    }
}