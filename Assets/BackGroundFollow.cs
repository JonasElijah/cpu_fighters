using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundFollow : MonoBehaviour
{
    public Transform playerOne;
    public Transform playerTwo;
    public FightManager fightManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (!playerOne && fightManager.playerOne)
        {
            playerOne = fightManager.playerOne.transform;
        }

        if (!playerTwo && fightManager.playerTwo)
        {
            playerTwo = fightManager.playerTwo.transform;
        }
        
    }

    void AdjustBackgroundPosition()
    {
        Vector3 middlePoint = GetMiddlePoint();
        Vector3 backGroundpos = new Vector3(middlePoint.x,middlePoint.y , transform.position.z);
        transform.position = Vector3.Lerp(transform.position, backGroundpos, 1.0f * Time.deltaTime);
    }   

     private Vector3 GetMiddlePoint()
    {
        if (playerOne == null || playerTwo == null)
        {
            return Vector3.zero;
        }

        return (playerOne.position + playerTwo.position) / 2f;
    }
}
