using UnityEngine;

public class AIStateMachine : MonoBehaviour
{
    public enum State
    {
        Idle,
        AttackOne,
        AttackTwo,
        Block,
        Moving
    }

    public Transform playerTransform; 
    public State currentState;
    public GameObject enemyGameObject;
    public Fighter fighter;
    public Fighter enemy;
    public float horizontalInput;

    private float actionCooldown = 0.1f; 
    private float lastActionTime; 

    public void ProcessState()
    {
         if (Vector3.Distance(fighter.getPosition(), enemy.getPosition()) < 1.5f)
            horizontalInput = 0;
         else
            horizontalInput = DetermineDirection();

        if (Time.time - lastActionTime > actionCooldown)
        {
            switch (currentState)
            {
                case State.Idle:
                    HandleIdle();
                    break;
                case State.Moving:
                    HandleMoving();
                    break;
                case State.AttackOne:
                    HandleAttackOne();
                    break;
                case State.AttackTwo:
                    HandleAttackTwo();
                    break;
                case State.Block:
                    HandleBlock();
                    break;
            }
        }
    }

    public void HandleIdle()
    {
        if(enemy.playerCombat.IsPunching || enemy.playerCombat.IsShooting)
        {
            SetState(State.Block);
        }

        if (Vector3.Distance(fighter.getPosition(), enemy.getPosition()) < 2.0f)
        {
            SetState(State.AttackOne);
        }
        else
        {
            if(ShouldShoot())
                SetState(State.AttackTwo);
            else
                SetState(State.Moving);
        }
    }

    public void HandleMoving()
    {
        fighter.HandleMovement(horizontalInput, false, false, KeyCode.Space);
        if(fighter.getPosition().x < -4.94f && fighter.getPosition().y < -2.2f)
        {
            fighter.HandleMovement(horizontalInput, true, true, KeyCode.Space);
        }

        if(horizontalInput == 0)
            SetState(State.Idle);
    }

    public void HandleAttackOne()
    {
        fighter.AttackOne();
        SetState(State.Idle);
    }

    private void HandleAttackTwo()
    {
        fighter.AttackTwo();
        SetState(State.Idle);
    }

    private void HandleBlock()
    {
        if(ShouldBlock())
            fighter.block(KeyCode.E);
        else
            fighter.HandleMovement(0, true, true, KeyCode.Space);

        SetState(State.Idle);
    }

    protected float DetermineDirection()
    {
        return enemy.rb.position.x > fighter.rb.position.x ? 1 : -1;
    }

    private void SetState(State newState)
    {
        currentState = newState;
        lastActionTime = Time.time;
    }

    private bool ShouldShoot()
    {
        return Random.Range(0, 100) > 50;
    }

    private bool ShouldBlock()
    {
        return Random.Range(0, 100) > 50;
    }
}
