using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerOneInput : MonoBehaviour
{
    public GameObject character;
    public GameObject healthBar;
    public Fighter fighter;
    public Slider slider;

    public float maxHealth = 10.0f;
    public float currentHealth;

    protected void Start()
    {
        currentHealth = maxHealth;
        // character = GetComponent<FightManager>().characters[CharacterSelectionHandler.playerOneCharacter];
        healthBar = GameObject.FindWithTag("PlayerOneHealthBar");
        slider = healthBar.GetComponent<Slider>();
        // fighter = character.GetComponent<Fighter>();
    }

    protected void Update()
    {
        GameManager.setPlayerOneHealth(currentHealth);
        fighter.HandleMovement(Input.GetAxisRaw("Horizontal_Player1"), Input.GetButtonDown("Jump_Player1"), Input.GetButton("Jump_Player1"), KeyCode.Space);
        if (Input.GetButtonDown("Attack1_Player1"))
        {
            fighter.Attack("PlayerOne");
        }
        slider.value = currentHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
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
}
