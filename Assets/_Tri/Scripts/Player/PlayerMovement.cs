using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float playerSpeed = 2.5f;
    public float runSpeed = 5f;
    public float currentSpeed;
    [SerializeField] float gravityValue = -9.81f;
    [SerializeField] float jumpHeight = 5f;
    public float HorizontalInput, VerticalInput;

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
        IsGrounded();
        Movement();
        Jump();
    }

    public void IsGrounded()
    {
        grounded = characterController.isGrounded;
        if (grounded && playerVelocity.y < 0)
        {
            playerVelocity.y = 0;
        }
    }

    public void Movement()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        move = new Vector3(input.x, 0, input.y);
        characterController.Move(move * Time.deltaTime * currentSpeed);

        HorizontalInput = input.x;
        VerticalInput = input.y;

        animator.SetFloat("HorizontalInput", HorizontalInput);
        animator.SetFloat("VerticalInput", VerticalInput);

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
