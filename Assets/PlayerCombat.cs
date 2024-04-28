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

    private float timeSinceLastAttackOne = 0;


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


    public void TryBlock(KeyCode blockCode)
    {

        if (player1)
        {
            player1.fighter.IsBlocking = true;
            Debug.Log("Player One Block: " + player1.fighter.IsBlocking);
        }

        if (player2)
        {
            player2.fighter.IsBlocking = true;
            Debug.Log("Player Two Block: " + player2.fighter.IsBlocking);
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
                PlayerTwoInput playertwo = enemy.GetComponent<PlayerTwoInput>();
                if (playertwo && enemy.tag == "PlayerTwo" && !playertwo.fighter.IsBlocking)
                {
                    playertwo.TakeDamage(player1.fighter.getAttackOneDamage());
                }
                else if (playertwo && enemy.tag == "PlayerTwo" && playertwo.fighter.IsBlocking)
                {
                    playertwo.TakeDamage(player1.fighter.getAttackOneDamage()/5);
                }
            }
        }

        if (attack == "PlayerTwo" && hitEnemies.Length != 0)
        {
            attackPoint = player2.fighter.attackPoint;
            foreach (Collider2D enemy in hitEnemies)
            {
                PlayerOneInput playerone = enemy.GetComponent<PlayerOneInput>();
                if (playerone && enemy.tag == "PlayerOne" && !playerone.fighter.IsBlocking)
                {
                    playerone.TakeDamage(player2.fighter.getAttackOneDamage());
                }
                else if (playerone && enemy.tag == "PlayerOne" && playerone.fighter.IsBlocking)
                {
                    playerone.TakeDamage(player2.fighter.getAttackOneDamage()/5);
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
