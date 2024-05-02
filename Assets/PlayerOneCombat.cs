using UnityEngine;

public class PlayerOneCombat : PlayerCombat
{
    private float timeSinceLastAttackOne = 0;
    private float timeSinceLastAttackTwo = 0;
    public PlayerOneInput player1;
    public GameObject projectile;


    void Start()
    {
        player1 = GetComponent<PlayerOneInput>();
    }

    void Update()
    {
        if (timeSinceLastAttackOne < player1.fighter.getAttackOneCooldown())
        {
            timeSinceLastAttackOne += Time.deltaTime;
        }

        if (timeSinceLastAttackTwo < player1.fighter.getAttackTwoCooldown())
        {
            IsShooting = false;
            timeSinceLastAttackTwo += Time.deltaTime;
        }

    }


    public override void AttackOne()
    {

        if (IsPunching || timeSinceLastAttackOne < player1.fighter.getAttackOneCooldown())
        {
            return;
        }

        IsPunching = true;
        timeSinceLastAttackOne = 0;

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, character);
        foreach (Collider2D enemy in hitEnemies)
        {
            PlayerTwoInput playerTwo = enemy.GetComponent<PlayerTwoInput>();
            if (playerTwo && enemy.tag == "PlayerTwo")
            {
                float damage = playerTwo.fighter.IsBlocking ? player1.fighter.getAttackOneDamage() / 5 : player1.fighter.getAttackOneDamage();
                playerTwo.TakeDamage(damage);
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

    public override void TryBlock(KeyCode blockCode)
    {
        player1.fighter.rb.velocity = new Vector3(0, player1.fighter.rb.velocity.y);;
        if (player1)
        {
            player1.fighter.IsBlocking = true;
            Debug.Log("Player One Block: " + player1.fighter.IsBlocking);
        }
    }

    public override void AttackTwo()
    {
        if (timeSinceLastAttackTwo < player1.fighter.getAttackTwoCooldown())
        {
            return;
        }
        IsShooting = true;
        timeSinceLastAttackTwo = 0;
        Vector3 firingDirection = player1.transform.localScale.x < 0 ? Vector3.left : Vector3.right;
        Quaternion firingRotation = Quaternion.LookRotation(Vector3.forward, firingDirection);
        GameObject projectileObject = Instantiate(projectile, projecttilePoint.position, firingRotation);
        Rigidbody2D rb = projectileObject.GetComponent<Rigidbody2D>();
        rb.AddForce(firingDirection * player1.fighter.getProjectileSpeed(), ForceMode2D.Impulse);
        Destroy(projectileObject, 15);
    }

}
