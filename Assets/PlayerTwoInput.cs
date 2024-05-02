using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTwoInput : PlayerInput
{
    public bool isAI = false;

    public float maxHealth = 10.0f;
    public float currentHealth;
    public float damageCooldown = 0.5f;

    public AIStateMachine stateMachine;

    void Start()
    {
        stateMachine = GetComponent<AIStateMachine>();
        stateMachine.fighter = fighter;
        stateMachine.enemyGameObject = GameObject.FindWithTag("PlayerOne");
        stateMachine.enemy = stateMachine.enemyGameObject.GetComponent<Fighter>();
        stateMachine.RL = GameObject.FindWithTag("RL").GetComponent<Transform>();
        stateMachine.LL = GameObject.FindWithTag("LL").GetComponent<Transform>();
        stateMachine.FZ = GameObject.FindWithTag("FZ").GetComponent<Transform>();

        currentHealth = maxHealth;
        healthBar = GameObject.FindWithTag("PlayerTwoHealthBar");
        playerIndicator = GameObject.FindWithTag("PlayerTwoIndicator");
        playerIndicator.GetComponent<PlayerIndicator>().target = fighter.playerIndicatePos;
        slider = healthBar.GetComponent<Slider>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        fighter.isPlayerTwo();
    }

    void Update()
    {
        GameManager.setPlayerTwoHealth(currentHealth);
        slider.value = currentHealth;

        if (isAI)
            HandleAIInput();
        else
            HandleHumanInput();
    }

    private void HandleHumanInput()
    {
        fighter.HandleMovement(Input.GetAxisRaw("Horizontal_Player2"), Input.GetButtonDown("Jump_Player2"), Input.GetButton("Jump_Player2"), KeyCode.I);
        if (Input.GetButtonDown("Attack1_Player2"))
        {
            fighter.AttackOne();
        }

        if (Input.GetButtonDown("Attack2_Player2"))
        {
            fighter.AttackTwo();
        }

        if (Input.GetButton("block_player2"))
        {
            fighter.block(KeyCode.U);
        }
    }

    private void HandleAIInput()
    {
        if (stateMachine != null)
        {
            stateMachine.ProcessState();
        }
    }

    private IEnumerator FlashColor(float duration)
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(duration);
        spriteRenderer.color = originalColor;
    }

    private bool ShouldBlock()
    {
        return Random.Range(0, 100) > 50;
    }

    // private bool ShouldAttack()
    // {
    //     return Random.Range(0, 100) > 30;
    // }

    public override void TakeDamage(float amount)
    {
        currentHealth -= amount;
        StartCoroutine(FlashColor(damageCooldown));
        if (currentHealth <= 0)
        {
            //Die
            //Play Death Animation
            //Transition to death screen   
        }
    }

}
