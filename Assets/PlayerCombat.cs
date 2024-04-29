using System;
using UnityEngine;

public abstract class PlayerCombat : MonoBehaviour
{
    public bool IsPunching;
    public Transform attackPoint;
    public Transform projecttilePoint;
    public float attackRange = 0.5f;
    public LayerMask character;
    public abstract void AttackOne();
    public abstract void AttackTwo();
    public abstract void TryBlock(KeyCode blockCode);

}
