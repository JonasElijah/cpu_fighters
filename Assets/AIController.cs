using UnityEngine;

public class AIController : MonoBehaviour
{
    public Transform playerTransform; 
    public float detectionRadius = 10.0f; 
    private Fighter fighter;
    private Rigidbody2D rb;
    private float speed = 5.0f;
    private float acceleration = 2.0f;
    private AnimationStateChanger[] animationStateChangers;

    void Start()
    {
        fighter = GetComponent<Fighter>();
        rb = GetComponent<Rigidbody2D>();
        animationStateChangers = GetComponentsInChildren<AnimationStateChanger>();
    }

    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) < detectionRadius)
        {
            float horizontalInput = DetermineDirection();
            fighter.MovePlayer(horizontalInput);
        }
    }

    protected float DetermineDirection()
    {
        // Determine direction based on player position relative to AI
        return Mathf.Sign(playerTransform.position.x - transform.position.x);
    }


}
