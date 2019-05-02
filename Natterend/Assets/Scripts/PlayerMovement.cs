using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSpeed = 2f;
    public float SprintMultiplier = 1.8f;

    public AudioClip[] StepSounds;

    [Space()]
    public Collider DefaultCollider;
    public Collider CrouchedCollider;

    Camera cam;
    Rigidbody body;
    AudioSource source;

    float horizontalRot;
    float verticalRot;

    public enum MovementType
    {
        Walking,
        Crouching,
        Sprinting
    }

    [HideInInspector] public MovementType currentMovement;
    float currentSpeed;

    void Start()
    {
        body = GetComponent<Rigidbody>();
        cam = GetComponentInChildren<Camera>();
        source = GetComponent<AudioSource>();

        horizontalRot = transform.eulerAngles.y;
        verticalRot = 0f;

        //Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentMovement = MovementType.Walking;
        currentSpeed = MoveSpeed;
    }

    void OnDestroy()
    {
        //Unlock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    #region Input
    private void Update()
    {
        if (currentMovement == MovementType.Walking)
            WalkingInput();
        else if (currentMovement == MovementType.Crouching && Input.GetButtonUp("Crouch"))
        {
            currentMovement = MovementType.Walking;
            currentSpeed = MoveSpeed;
            //Move camera up
            cam.transform.DOLocalMoveY(2f, .25f);
            //Change collider
            DefaultCollider.enabled = true;
            CrouchedCollider.enabled = false;
        }
        else if (currentMovement == MovementType.Sprinting && Input.GetButtonUp("Sprint") || !CoffeeController.Instance.CanSprint())
        {
            currentMovement = MovementType.Walking;
            currentSpeed = MoveSpeed;
        }
    }

    void WalkingInput()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            currentMovement = MovementType.Crouching;
            currentSpeed = MoveSpeed * .5f;

            //Move camera down
            cam.transform.DOLocalMoveY(1f, .25f);
            //Change collider
            CrouchedCollider.enabled = true;
            DefaultCollider.enabled = false;
        }
        else if (Input.GetButtonDown("Sprint") && CoffeeController.Instance.CanSprint())
        {
            currentMovement = MovementType.Sprinting;
            currentSpeed = MoveSpeed * SprintMultiplier;
        }
    }

    #endregion

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

    void Movement()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        //Remember to normalize, so we can't move faster with strafe + forward movement
        Vector3 move = transform.rotation * input.normalized;

        if (move != Vector3.zero)
        {
            body.MovePosition(body.position + move * currentSpeed * Time.fixedDeltaTime);

            //Play sounds
            PlayStepSounds();
        }
    }

    int stepIndex = 0;
    float cooldown = 0f;

    float stepDuration = 2f;

    void PlayStepSounds() {
        cooldown += Time.deltaTime * stepDuration * (currentMovement == MovementType.Sprinting ? SprintMultiplier : 1f);
       
        if (cooldown >= 1f)
        {
            cooldown = 0f;
            //Play
            source.PlayOneShot(StepSounds[stepIndex]);
            //Increment
            stepIndex = (stepIndex + 1) % StepSounds.Length;
        }
    }
}
