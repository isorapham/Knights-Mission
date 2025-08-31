using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed = 5f;
    [SerializeField]
    private float runSpeed = 10f;
    Vector2 moveInput;
    public float CurrentMoveSpeed
    {
        get
        {
            if (IsMoving)
            {
                if (IsRunning)
                {
                    return runSpeed;
                }
                else
                {
                    return walkSpeed;
                }
            }
            else
            {
                return 0; // Idle
            }
        }
    }
    [SerializeField]
    private bool _isMoving = false;
    public bool IsMoving
    {
        get
        { 
            return _isMoving; 
        }
        private set
        {
            _isMoving = value;
            animator.SetBool("isMoving", value);
        }
    }

    [SerializeField]
    private bool _isRunning = false;
    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        set
        {
            _isRunning = value;
            animator.SetBool("isRunning", value);
        }
    }

    public bool _isFacingRight = true;
    public bool IsFacingRight { get
        {
            return _isFacingRight;
        }
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight=value;
        }
        }

    public Rigidbody2D rb;
    Animator animator;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.linearVelocity.y);
        animator = GetComponent<Animator>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
        SetFacingDirection(moveInput);
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
            //face right
        }
        else if(moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
            //face left
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }

    }
}
