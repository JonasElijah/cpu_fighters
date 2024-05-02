public class MatteoFighter : Fighter
{   
    protected float attackOneCooldown = 1f;
    protected float attackTwoCooldown = 1.5f;
    protected float attackOneDamage = 0.5f;
    protected float projectTileSpeed = 10.0f;

    public MatteoFighter()
    {
        this.speed = 10.0f;
        this.acceleration = 50.0f;
        this.jumpForce = 10.0f;
        this.jumpTime = 0.25f;
    }

    public override float getAttackOneCooldown()
    {
        return this.attackOneCooldown;
    }

    public override float getAttackTwoCooldown()
    {
        return this.attackTwoCooldown;
    }

    public override float getAttackOneDamage()
    {
        return this.attackOneDamage;
    }

    public override float getProjectileSpeed()
    {
        return this.projectTileSpeed;
    }

}