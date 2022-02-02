using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementComponent : MonoBehaviour
{
    // Movement variables
    [SerializeField]
    private float walkSpeed = 5;
    [SerializeField]
    private float runSpeed = 10;
    [SerializeField]
    private float jumpForce = 5;
    [SerializeField]
    private Transform followTransform;
    [SerializeField]
    private float aimSensitivity = 1f;
    private Vector2 rotationVector;

    // Components
    private PlayerController playerController;
    private Rigidbody rigidBody;
    private Animator playerAnimator;

    // Movement references
    Vector2 inputVector = Vector2.zero;
    Vector3 moveDirection = Vector3.zero;
    Vector2 lookInput = Vector2.zero;

    public readonly int movementXHash = Animator.StringToHash("MovementX");
    public readonly int movementYHash = Animator.StringToHash("MovementY");
    public readonly int isRunningHash = Animator.StringToHash("isRunning");
    public readonly int isJumpingHash = Animator.StringToHash("isJumping");
    public readonly int isFiringHash = Animator.StringToHash("isFiring");
    public readonly int isReloadingingHash = Animator.StringToHash("isReloading");

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        rigidBody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        //camera
        rotationVector += new Vector2(-lookInput.y, lookInput.x) * aimSensitivity;
        rotationVector.x = Mathf.Clamp(rotationVector.x, -80.0f, 80.0f);

        transform.rotation = Quaternion.Euler(0f, rotationVector.y, 0.0f);
        followTransform.localRotation = Quaternion.Euler(rotationVector.x, 0f, 0f);

        // Movement
        moveDirection = transform.forward * inputVector.y + transform.right * inputVector.x;
        float currentSpeed = playerController.isRunning ? runSpeed : walkSpeed;

        Vector3 moveVelocity = moveDirection * currentSpeed;
        if (playerController.isJumping)
        {
            rigidBody.AddForce(moveVelocity * Time.deltaTime, ForceMode.VelocityChange);
        }
        else
        {
            rigidBody.velocity = moveVelocity + rigidBody.velocity.y * Vector3.up;
        }

        playerAnimator.SetFloat(movementXHash, inputVector.x, 1, 0.1f);
        playerAnimator.SetFloat(movementYHash, inputVector.y, 1, 0.1f);
    }

    public void OnMovement(InputValue value)
    {
        inputVector = value.Get<Vector2>();
    }

    public void OnRun(InputValue value)
    {
        playerController.isRunning = value.isPressed;
        playerAnimator.SetBool(isRunningHash, value.isPressed);
    }

    public void OnJump(InputValue value)
    {
        if (playerController.isJumping) return;

        playerController.isJumping = true;
        rigidBody.AddForce(transform.up * jumpForce + moveDirection * walkSpeed * Time.deltaTime, ForceMode.Impulse);
        playerAnimator.SetBool(isJumpingHash, true);
    }

    public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>();
        // If we aim up, down, adjust animations to have a mask that will let us properly animate aim
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Ground") || !playerController.isJumping) return;

        playerController.isJumping = false;
        playerAnimator.SetBool(isJumpingHash, false);
    }
}
