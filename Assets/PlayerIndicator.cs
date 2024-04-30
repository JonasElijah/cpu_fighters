using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerIndicator : MonoBehaviour
{
    public Transform target; 

    void Update()
    {
        if(target != null)
        {
            Vector3 screenPosition = target.position;
            transform.position = screenPosition;
        }
        else
        {
            Debug.Log("Target is null\n");
        }
    }
}
