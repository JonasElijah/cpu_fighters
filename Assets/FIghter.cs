using UnityEngine;
using System.Collections.Generic;
using System;

public abstract class Fighter : MonoBehaviour
{
    // Stats
    public PlayerCombat playerCombat;
    protected float speed;
    protected float jumpForce;
    protected float moveInput;
    protected bool isFacingRight = true;
    protected bool isJumping;
    protected float jumpTimeCounter;
    protected float jumpTime;
    protected bool isMoving;
    protected bool isGrounded;
    protected bool touchingCharacter;
    private float currentVelocity;
    public bool IsBlocking;
    protected KeyCode blockKey;




    // Nerd Info
    [Header("Nerd Info")]
    [SerializeField] protected List<AnimationStateChanger> animationStateChangers;
    public bool isPlayerOne;
    [SerializeField] protected Transform feetPos;
    [SerializeField] protected float checkRadius;
    public Transform attackPoint;
    [SerializeField] protected Animator animator;
    [SerializeField] protected LayerMask ground;
    [SerializeField] protected LayerMask character;


    protected Rigidbody2D rb;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerCombat = GetComponent<PlayerCombat>();
    }

    protected void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, 0.5f, ground);

        if (playerCombat.IsPunching)
        {
            foreach (AnimationStateChanger asc in animationStateChangers)
            {
                asc.ChangeAnimationState("Punching");
            }
        }

        if (IsIdle())
        {
            foreach (AnimationStateChanger asc in animationStateChangers)
            {
                asc.ChangeAnimationState("Idle");
            }
        }

        if (Input.GetKeyUp(blockKey))
        {
            {
                IsBlocking = false;
                Debug.Log("Player stopped blocking.");
            }
        }

    }

    private void HandleJumping(bool jumpInput, bool jumpHoldInput, KeyCode jumpCode)
    {
        if (jumpInput && isGrounded && !playerCombat.IsPunching)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;

            float horizontalVelocity = rb.velocity.x;
            rb.velocity = new Vector2(horizontalVelocity, jumpForce);
        }

        if (jumpHoldInput && isJumping)
        {
            if (jumpTimeCounter > 0)
            {
                float horizontalVelocity = rb.velocity.x;
                rb.velocity = new Vector2(horizontalVelocity, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(jumpCode))
        {
            isJumping = false;
        }
    }

    public void HandleMovement(float horizontalInput, bool jumpInput, bool jumpHoldInput, KeyCode jumpCode)
    {
        if(IsBlocking) return;

        if (playerCombat)
        {
            Flip(horizontalInput);
            moveInput = horizontalInput;

            Vector3 input = new Vector3(moveInput, rb.velocity.y, 0);
            if (!playerCombat.IsPunching)
                MovePlayer(input, horizontalInput);

            HandleJumping(jumpInput, jumpHoldInput, jumpCode);
        }
    }


    public void AttackOne(String attack)
    {
        if(IsBlocking) return;
        playerCombat.AttackOne(attack);
    }

    public void block(KeyCode blockCode)
    {
        blockKey = blockCode;
        playerCombat.TryBlock(blockCode);
    }

    public bool IsIdle()
    {
        return !(playerCombat.IsPunching || isJumping || isMoving);
    }

    protected void MovePlayer(Vector3 direction, float horizontalInput)
    {
        float targetSpeed = horizontalInput * speed;
        float smoothedSpeed = Mathf.SmoothDamp(rb.velocity.x, targetSpeed, ref currentVelocity, 0.05f);
        rb.velocity = new Vector3(smoothedSpeed, rb.velocity.y);

        isMoving = Mathf.Abs(horizontalInput) > 0.01f;
        if (isMoving)
        {
            foreach (AnimationStateChanger asc in animationStateChangers)
            {
                asc.ChangeAnimationState("Walk");
            }
        }
    }

    protected void Flip(float horizontalInput)
    {
        if ((isFacingRight && horizontalInput < 0) || (!isFacingRight && horizontalInput > 0))
        {
            flipCharacter();
        }
    }

    public bool CanBlock()
    {
        return !isJumping;
    }

    // public void setPlayer(bool x)
    // {
    //     isPlayerOne = x;
    // }

    public void flipCharacter()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1.0f;
        transform.localScale = localScale;
    }

    public void isPlayerTwo()
    {
        isFacingRight = !isFacingRight;
    }

    public abstract float getAttackOneCooldown();
    public abstract float getAttackOneDamage();

}

