using UnityEngine;
using System.Collections.Generic;
using System;
using System.Data;

public abstract class Fighter : MonoBehaviour
{
    // Stats
    public PlayerCombat playerCombat;
    protected float speed;
    protected float acceleration;
    protected float jumpForce;
    protected float moveInput;
    protected bool isFacingRight = true;
    protected bool isJumping;
    protected float jumpTimeCounter;
    protected float jumpTime;
    protected bool isMoving;
    protected bool isGrounded;
    protected bool isPlatform;
    protected bool touchingCharacter;
    private float currentVelocity;
    public bool IsBlocking;
    private int jumpCount = 0; 
    private int maxJumps = 1; 
    protected KeyCode blockKey;


    // Nerd Info
    [Header("Nerd Info")]
    [SerializeField] protected List<AnimationStateChanger> animationStateChangers;
    public bool isPlayerOne;
    [SerializeField] protected Transform feetPos;
    [SerializeField] public Transform playerIndicatePos;
    [SerializeField] protected Transform projectilePos;
    [SerializeField] protected float checkRadius;
    public Transform attackPoint;
    [SerializeField] protected Animator animator;
    [SerializeField] protected LayerMask ground;
    [SerializeField] protected LayerMask platform;
    [SerializeField] protected LayerMask character;
    public SpriteRenderer blockSprite;


    public Rigidbody2D rb;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.mass = 900;
        animator = GetComponent<Animator>();
        playerCombat = GetComponent<PlayerCombat>();
        blockSprite.enabled =false;
    
    }

    protected void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, 0.5f, ground);
        //isPlatform = Physics2D.OverlapCircle(feetPos.position, 0.5f, platform);
        //Debug.Log(isPlatform);
        if(IsBlocking)
        {
            blockSprite.enabled =true;
        }
        else
        {
            blockSprite.enabled =false;
        }

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

    public void playAttackOneAnimation()
    {
        foreach (AnimationStateChanger asc in animationStateChangers)
        {
            asc.ChangeAnimationState("Punching");
        }
    }

    

    private void HandleJumping(bool jumpInput, bool jumpHoldInput, KeyCode jumpCode)
    {
        if (jumpInput && jumpCount < maxJumps && !playerCombat.IsPunching)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;

            float horizontalVelocity = rb.velocity.x;
            rb.velocity = new Vector2(horizontalVelocity, jumpForce);
            rb.drag = 0;  

            jumpCount++;  
        }

        if (jumpHoldInput && isJumping && jumpTimeCounter > 0)
        {
            float horizontalVelocity = rb.velocity.x;
            rb.velocity = new Vector2(horizontalVelocity, jumpForce);
            jumpTimeCounter -= Time.deltaTime;
        }
        else
        {
            isJumping = false;
            rb.drag = 1;  
        }

        if (Input.GetKeyUp(jumpCode) && isJumping)
        {
            isJumping = false;
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);  
            }
            rb.drag = 1;  
        }

        if (isGrounded)
        {
            jumpCount = 0;
        }
    }


    public void HandleMovement(float horizontalInput, bool jumpInput, bool jumpHoldInput, KeyCode jumpCode)
    {
        if (IsBlocking) return;
        Debug.Log("Horizontal Input: " + horizontalInput);

        if (playerCombat)
        {
            moveInput = horizontalInput;

            if (!playerCombat.IsPunching)
                MovePlayer(horizontalInput);

            HandleJumping(jumpInput, jumpHoldInput, jumpCode);
        }
    }


    public void AttackOne()
    {
        if (IsBlocking) return;
        playerCombat.AttackOne();
    }

    public void AttackTwo()
    {
        if (IsBlocking) return;
        playerCombat.AttackTwo();
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

    public void MovePlayer(float horizontalInput)
    {
        Flip(horizontalInput);
        float maxSpeed = speed; 
        float targetSpeed = horizontalInput * maxSpeed;
        float currentSpeed = Mathf.MoveTowards(rb.velocity.x, targetSpeed, acceleration * Time.deltaTime);
        rb.velocity = new Vector3(currentSpeed, rb.velocity.y);

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

    public Vector3 getPosition()
    {
        return rb.position;
    }

    public abstract float getAttackOneCooldown();
    public abstract float getAttackOneDamage();
    public abstract float getProjectileSpeed();
    public abstract float getAttackTwoCooldown();

}

