using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 2f;
    public float SprintMultiplier = 1.8f;

    Camera cam;
    Rigidbody body;

    float horizontalRot;
    float verticalRot;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();

        horizontalRot = transform.eulerAngles.y;
        verticalRot = 0f;

        //Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void OnDestroy()
    {
        //Unlock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void FixedUpdate()
    {
        //Don't move
        body.velocity = new Vector3(0f, body.velocity.y, 0f);
        body.angularVelocity = Vector3.zero;

        MouseLook();
        Movement();
    }

    float mouseSensitivity = 3f;

    void MouseLook()
    {
        horizontalRot += Input.GetAxis("Mouse X") * mouseSensitivity;
        //Invert
        verticalRot += -Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRot = Mathf.Clamp(verticalRot, -80f, 80f);

        transform.rotation = Quaternion.Euler(0f, horizontalRot, 0f);
        cam.transform.localRotation = Quaternion.Euler(verticalRot, 0f, 0f);
    }

    [HideInInspector] public bool IsSprinting = false;

    void Movement()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        //Remember to normalize, so we can't move faster with strafe + forward movement
        Vector3 move = transform.rotation * input.normalized;

        if (move != Vector3.zero)
        {
            float speed = IsSprinting ? MoveSpeed * SprintMultiplier : MoveSpeed;
            body.MovePosition(body.position + move * speed * Time.fixedDeltaTime);
        }
    }
}
