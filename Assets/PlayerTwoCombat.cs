using UnityEngine;

public class PlayerTwoCombat : PlayerCombat
{
    private float timeSinceLastAttackOne = 0;
    private float timeSinceLastAttackTwo = 0;
    public PlayerTwoInput player2;
    public GameObject projectile;


    void Start()
    {
        player2 = GetComponent<PlayerTwoInput>();
    }

    void Update()
    {
        if (player2 && timeSinceLastAttackOne < player2.fighter.getAttackOneCooldown())
        {
            timeSinceLastAttackOne += Time.deltaTime;
        }

        if (timeSinceLastAttackTwo < player2.fighter.getAttackTwoCooldown())
        {
            timeSinceLastAttackTwo += Time.deltaTime;
        }
    }

    public override void AttackOne()
    {
        if (IsPunching || timeSinceLastAttackOne < player2.fighter.getAttackOneCooldown())
        {
            return;
        }

        IsPunching = true;
        timeSinceLastAttackTwo = 0;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, character);
        foreach (Collider2D enemy in hitEnemies)
        {
            PlayerOneInput playerOne = enemy.GetComponent<PlayerOneInput>();
            if (playerOne && enemy.tag == "PlayerOne")
            {
                float damage = playerOne.fighter.IsBlocking ? player2.fighter.getAttackOneDamage() / 5 : player2.fighter.getAttackOneDamage();
                playerOne.TakeDamage(damage);
            }
        }
    }

    public override void TryBlock(KeyCode blockCode)
    {
        if (player2)
        {
            player2.fighter.IsBlocking = true;
            Debug.Log("Player One Block: " + player2.fighter.IsBlocking);
        }
    }

    public void OnPunchAnimationEnd()
    {
        IsPunching = false;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }

    public override void AttackTwo()
    {
        if (timeSinceLastAttackTwo < player2.fighter.getAttackTwoCooldown())
        {
            return;
        }
        timeSinceLastAttackTwo = 0;
        Vector3 firingDirection = player2.transform.localScale.x < 0 ? Vector3.left : Vector3.right;
        Quaternion firingRotation = Quaternion.LookRotation(Vector3.forward, firingDirection);
        GameObject projectileObject = Instantiate(projectile, projecttilePoint.position, firingRotation);
        Rigidbody2D rb = projectileObject.GetComponent<Rigidbody2D>();
        rb.AddForce(firingDirection * player2.fighter.getProjectileSpeed(), ForceMode2D.Impulse);
        Destroy(projectileObject, 15);
    }

}
