using UnityEngine;
using System.Collections.Generic;

public class MatteoFighter : Fighter
{
    protected float attackOneCooldown = 1f; 
    protected float attackOneDamage = 0.5f;
    
    public MatteoFighter()
    {
        this.speed = 10.0f;
        this.jumpForce = 10.0f;
        this.jumpTime = 0.25f;
    }

    public override float getAttackOneCooldown()
    {
       return this.attackOneCooldown;
    }

    public override float getAttackOneDamage()
    {
        return this.attackOneDamage;
    }
}