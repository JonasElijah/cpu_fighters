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

    void Start()
    {
        player1 = GetComponent<PlayerOneInput>();
        player2 = GetComponent<PlayerTwoInput>();
    }

    public void Attack(String attack)
    {
        IsPunching = true;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, character);


        if (attack == "PlayerOne" && hitEnemies.Length != 0)
        {
            attackPoint = player1.fighter.attackPoint;
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.tag == "PlayerTwo")
                {
                    //Debug.Log("Enemy: " + enemy.tag);
                    enemy.GetComponent<PlayerTwoInput>().TakeDamage(0.1f);
                }
            }
        }

        if (attack == "PlayerTwo" && hitEnemies.Length != 0)
        {
            attackPoint = player2.fighter.attackPoint;
            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log(enemy.tag);
                if (enemy.tag == "PlayerOne")
                {
                    enemy.GetComponent<PlayerOneInput>().TakeDamage(0.1f);
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

