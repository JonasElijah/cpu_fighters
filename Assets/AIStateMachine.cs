using System;
using System.Collections.Generic;
using UnityEngine;

// public class AIStateMachine : MonoBehaviour
// {
    

   

//     public void ProcessState()
//     {
//         // if (Vector3.Distance(fighter.getPosition(), enemy.getPosition()) < 1.5f)
//         //     horizontalInput = 0;
//         // else
//         //     horizontalInput = DetermineDirection();

//         float squaredDist = (fighter.getPosition() - enemy.getPosition()).sqrMagnitude;
//         if (squaredDist < 2.25f) 
//             horizontalInput = 0;
//         else
//             horizontalInput = DetermineDirection();


//         Debug.Log("VELOCITY: " + fighter.rb.velocity);
//         if (Time.time - lastActionTime > actionCooldown)
//         {
//             switch (currentState)
//             {
//                 case State.Idle:
//                     HandleIdle();
//                     break;
//                 case State.Moving:
//                     HandleMoving();
//                     break;
//                 case State.AttackOne:
//                     HandleAttackOne();
//                     break;
//                 case State.AttackTwo:
//                     HandleAttackTwo();
//                     break;
//                 case State.Block:
//                     HandleBlock();
//                     break;
//             }
//         }
//     }

//     public void HandleIdle()
//     {
        
//     }

//     public void HandleMoving()
//     {
        
//     }

//     public void HandleAttackOne()
//     {
        
//     }

//     private void HandleAttackTwo()
//     {
        
//     }

//     private void HandleBlock()
//     {
        
// }




//     private void SetState(State newState)
// {
//     if (currentState != newState)
//     {
//         currentState = newState;
//         lastActionTime = Time.time;
//         Debug.Log("State changed to: " + newState);
//     }
// }





// }


public class AIStateMachine : MonoBehaviour
{
    public Transform playerTransform; 
    public State currentState;
    public GameObject enemyGameObject;
    public Fighter fighter;
    public Fighter enemy;
    public float horizontalInput;
    public Transform LL;
    public Transform RL;
    public Transform FZ;

    public float actionCooldown;
    private float lastActionTime; 

    private Dictionary<State, Action> stateActions;

    public enum State
    {
        Idle,
        AttackOne,
        AttackTwo,
        Block,
        Moving
    }

    private void Start()
    {
        InitializeStateActions();
    }

    private void InitializeStateActions()
    {
        stateActions = new Dictionary<State, Action>
        {
            { State.Idle, HandleIdle },
            { State.Moving, HandleMoving },
            { State.AttackOne, HandleAttackOne },
            { State.AttackTwo, HandleAttackTwo },
            { State.Block, HandleBlock }
        };
    }

    public void ProcessState()
    {
        if(fighter.IsBlocking && currentState != State.Block)
        {
            fighter.IsBlocking = false;
        }

        float squaredDist = (fighter.getPosition() - enemy.getPosition()).sqrMagnitude;
        if (squaredDist < 2.25f) 
            horizontalInput = 0;
        else
            horizontalInput = DetermineDirection();

        if (Time.time - lastActionTime > actionCooldown)
        {
            stateActions[currentState]();
        }
    }

    private void HandleIdle()
    {
        fighter.rb.velocity = Vector3.zero;
        if(enemy.playerCombat.IsPunching || enemy.playerCombat.IsShooting)
        {
            SetState(State.Block);
        }

        if (Vector3.Distance(fighter.getPosition(), enemy.getPosition()) < 1.5f)
        {
            if (!IsFacingTarget())
            {
                TurnTowardsTarget();
            }
            else
            {
                SetState(State.AttackOne);
            }
        }
        else
        {
            if(ShouldShoot())
                SetState(State.AttackTwo);
            else
                SetState(State.Moving);
        }    
    }

    private void HandleMoving()
    {
        fighter.HandleMovement(horizontalInput, false, false, KeyCode.Space);
        if((fighter.getPosition().y < enemy.getPosition().y) && ShouldJump())
        {
            fighter.HandleMovement(horizontalInput, true, true, KeyCode.Space);
        }
        
        if(fighter.getPosition().x < LL.position.x && fighter.getPosition().y < FZ.position.y)
        {
            fighter.HandleMovement(horizontalInput, true, true, KeyCode.Space);
        }

        if(fighter.getPosition().x < RL.position.x && fighter.getPosition().y < FZ.position.y)
        {
            fighter.HandleMovement(horizontalInput, true, true, KeyCode.Space);
        }


        if(horizontalInput == 0)
            SetState(State.Idle);    
    }

    private void HandleAttackOne()
    {
        fighter.rb.velocity = Vector3.zero;
        fighter.AttackOne();
        SetState(State.Idle);
    }

    private void HandleAttackTwo()
    {
        fighter.rb.velocity = Vector3.zero;
        fighter.AttackTwo();
        SetState(State.Idle);
    }

    private void HandleBlock()
    {
        Debug.Log("Handling Block: Enemy Punching: " + enemy.playerCombat.IsPunching + ", Enemy Shooting: " + enemy.playerCombat.IsShooting);
        fighter.rb.velocity = Vector3.zero;
        if (ShouldBlock())
        {
            fighter.block(KeyCode.U);
            SetState(State.Idle); 
        }
        
        SetState(State.Idle); 
    }

    private bool ShouldShoot()
    {
        return UnityEngine.Random.Range(0, 100) > 50;
    }

    private bool ShouldJump()
    {
        return UnityEngine.Random.Range(0, 100) > 50;
    }

    private bool ShouldBlock()
    {
        return UnityEngine.Random.Range(0, 100) > 50;
    }
   
    private void TurnTowardsTarget()
    {
        Vector3 scale = fighter.transform.localScale;
        scale.x = -scale.x; 
        fighter.transform.localScale = scale;
    }

    public bool IsFacingTarget()
    {
        bool isFacingRight = fighter.transform.localScale.x > 0;
        bool enemyIsToRight = enemy.transform.position.x > fighter.transform.position.x;

        return isFacingRight == enemyIsToRight;
    }

    protected float DetermineDirection()
    {
        return enemy.rb.position.x > fighter.rb.position.x ? 1 : -1;
    }

    private void SetState(State newState)
    {
        if (currentState != newState)
        {
            currentState = newState;
            lastActionTime = Time.time;
            Debug.Log($"State changed to: {newState}");
        }
    }
}
