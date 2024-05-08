using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOneInput : PlayerInput
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
        stateMachine.enemyGameObject = GameObject.FindWithTag("PlayerTwo");
        stateMachine.enemy = stateMachine.enemyGameObject.GetComponent<Fighter>();
        stateMachine.RL = GameObject.FindWithTag("RL").GetComponent<Transform>();
        stateMachine.LL = GameObject.FindWithTag("LL").GetComponent<Transform>();
        stateMachine.FZ = GameObject.FindWithTag("FZ").GetComponent<Transform>();
        stateMachine.actionCooldown = CharacterSelectionHandler.aiDifficultyp1;

        currentHealth = maxHealth;
        healthBar = GameObject.FindWithTag("PlayerOneHealthBar");
        playerIndicator = GameObject.FindWithTag("PlayerOneIndicator");
        playerIndicator.GetComponent<PlayerIndicator>().target = fighter.playerIndicatePos;
        slider = healthBar.GetComponent<Slider>();

        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    void LateUpdate()
    {
        GameManager.setPlayerOneHealth(currentHealth);
        slider.value = currentHealth;

        if (isAI)
            HandleAIInput();
        else
            HandleHumanInput();
    }

    private void HandleHumanInput()
    {
        if (!PauseMenu.isPaused)
            fighter.HandleMovement(Input.GetAxisRaw("Horizontal_Player1"), Input.GetButtonDown("Jump_Player1"), Input.GetButton("Jump_Player1"), KeyCode.Space);
        if (Input.GetButtonDown("Attack1_Player1") && !PauseMenu.isPaused)
        {
            fighter.AttackOne();
        }

        if (Input.GetButtonDown("Attack2_Player1") && !PauseMenu.isPaused)
        {
            fighter.AttackTwo();
        }

        if (Input.GetButton("block_player1") && !PauseMenu.isPaused)
        {
            fighter.block(KeyCode.E);
        }
    }

    private void HandleAIInput()
    {
        if (stateMachine != null && !PauseMenu.isPaused)
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
