using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float playerSpeed = 2.5f;
    public float runSpeed = 5f;
    public float currentSpeed;
    [SerializeField] float gravityValue = -9.81f;
    [SerializeField] float jumpHeight = 5f;
    public float HorizontalInput, VerticalInput;

    [Header("Animation Smoothing")]
    private float damping = 0.075f;
    [SerializeField]
    public float rotationSpeed = 0.1f;


    public Animator animator;
    private CharacterController characterController;
    private PlayerInput playerInput;
    private Vector3 playerVelocity;
    public Vector3 move;
    private bool grounded;

    private InputAction moveAction;
    private InputAction jumpAction;


    [Header("StateManagement")]
    MovementBaseStates currentState;
    public IdleState Idle = new IdleState();
    public WalkState Walk = new WalkState();
    public RunState Run = new RunState();


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        animator = GetComponent<Animator>();
        SwitchState(Idle);
        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        IsGrounded();
        Movement();
        Jump();
    }

    public void IsGrounded()
    {
        grounded = characterController.isGrounded;
        if (grounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -1;
        }
    }

    public void Movement()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        move = new Vector3(input.x, 0, input.y);

        if (input == Vector2.zero)
        {
            damping = 0;
        }
        else
        {
            damping = 0.075f;
        }

        var cam = Camera.main;
        var forward = cam.transform.forward;
        var right = cam.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        move = (forward * input.y + right * input.x).normalized;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(move), rotationSpeed);

        characterController.Move(move * Time.deltaTime * currentSpeed);

        HorizontalInput = input.x;
        VerticalInput = input.y;

        if (move != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }

        animator.SetFloat("HorizontalInput", HorizontalInput, damping, Time.deltaTime);
        animator.SetFloat("VerticalInput", VerticalInput, damping, Time.deltaTime);

        playerVelocity.y += gravityValue * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
        currentState.UpdateState(this);

        Debug.Log(currentSpeed);
    }

    public void Jump()
    {
        if (jumpAction.triggered && grounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            Debug.Log("Jump!!!");
        }
    }

    public void SwitchState(MovementBaseStates state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
