using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateChanger : MonoBehaviour
{
    
    [SerializeField] private Animator animator;
    [SerializeField] private string currentState;

    public void ChangeAnimationState(string newState)
    {
        if(currentState == newState)
            return;
        currentState = newState;
        animator.Play(currentState);
    }

    public string GetCurrentAnimationState()
    {
        return currentState;
    }
}

