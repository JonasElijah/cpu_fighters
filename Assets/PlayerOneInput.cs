using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerOneInput : PlayerInput
{
    public float maxHealth = 10.0f;
    public float currentHealth;
    public float damageCooldown = 0.5f;

    protected void Start()
    {
        currentHealth = maxHealth;
        // character = GetComponent<FightManager>().characters[CharacterSelectionHandler.playerOneCharacter];
        healthBar = GameObject.FindWithTag("PlayerOneHealthBar");
        playerIndicator = GameObject.FindWithTag("PlayerOneIndicator");
        playerIndicator.GetComponent<PlayerIndicator>().target = fighter.playerIndicatePos;
        slider = healthBar.GetComponent<Slider>();
        // fighter = character.GetComponent<Fighter>();
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    protected void Update()
    {
        GameManager.setPlayerOneHealth(currentHealth);
        if(!PauseMenu.isPaused)
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

        slider.value = currentHealth;
    }

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

    public float GetHealth()
    {
        return currentHealth;
    }

    public Fighter GetFighter()
    {
        return fighter;
    }

    private IEnumerator FlashColor(float duration)
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(duration);
        spriteRenderer.color = originalColor;
    }
}
