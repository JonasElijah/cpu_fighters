using System;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public bool IsPunching;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask character;
    public PlayerOneInput player1;
    public PlayerTwoInput player2;

    //public float attackOneCooldown = 1f; // Cooldown period in seconds
    private float timeSinceLastAttackOne = 0; // Time since last attack

    void Start()
    {
        player1 = GetComponent<PlayerOneInput>();
        player2 = GetComponent<PlayerTwoInput>();
    }

    void Update()
    {
        if (player1 && timeSinceLastAttackOne < player1.fighter.getAttackOneCooldown())
        {
            timeSinceLastAttackOne += Time.deltaTime;
        }

        if (player2 && timeSinceLastAttackOne < player2.fighter.getAttackOneCooldown())
        {
            timeSinceLastAttackOne += Time.deltaTime;
        }
    }

    public void AttackOne(String attack)
    {
        if (player1 && (IsPunching || timeSinceLastAttackOne < player1.fighter.getAttackOneCooldown()))
        {
            return;
        }

        if (player2 && (IsPunching || timeSinceLastAttackOne < player2.fighter.getAttackOneCooldown()))
        {
            return;
        }

        IsPunching = true;
        timeSinceLastAttackOne = 0; // Reset the timer

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, character);
        if (attack == "PlayerOne" && hitEnemies.Length != 0)
        {
            attackPoint = player1.fighter.attackPoint;
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.tag == "PlayerTwo")
                {
                    enemy.GetComponent<PlayerTwoInput>().TakeDamage(player1.fighter.getAttackOneDamage());
                }
            }
        }

        if (attack == "PlayerTwo" && hitEnemies.Length != 0)
        {
            attackPoint = player2.fighter.attackPoint;
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.tag == "PlayerOne")
                {
                    enemy.GetComponent<PlayerOneInput>().TakeDamage(player2.fighter.getAttackOneDamage());
                }
            }
        }
    }

    public void OnPunchAnimationEnd()
    {
        IsPunching = false;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
