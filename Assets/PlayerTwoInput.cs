using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

public class PlayerTwoInput : MonoBehaviour
{
    public GameObject character;
    public GameObject healthBar;
    public Fighter fighter;
    public Slider slider;
    public SpriteRenderer spriteRenderer;
    private Color originalColor;


    public float maxHealth = 10.0f;
    public float currentHealth;
    public float damageCooldown = 0.5f;


    protected void Start()
    {
        currentHealth = maxHealth;
        healthBar = GameObject.FindWithTag("PlayerTwoHealthBar");
        slider = healthBar.GetComponent<Slider>();
        // fighter = character.GetComponent<Fighter>();
        //fighter.flipCharacter();
        if (spriteRenderer == null)
            spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        fighter.isPlayerTwo();
    }

    protected void Update()
    {
        GameManager.setPlayerTwoHealth(currentHealth);
        fighter.HandleMovement(Input.GetAxisRaw("Horizontal_Player2"), Input.GetButtonDown("Jump_Player2"), Input.GetButton("Jump_Player2"), KeyCode.I);
        if (Input.GetButtonDown("Attack1_Player2"))
        {
            fighter.AttackOne("PlayerTwo");
        }

        if (Input.GetButton("block_player2"))
        {
            fighter.block(KeyCode.U);
        }

        slider.value = currentHealth;
    }

    public void TakeDamage(float amount)
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

    private IEnumerator FlashColor(float duration)
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(duration);
        spriteRenderer.color = originalColor;
    }
}
