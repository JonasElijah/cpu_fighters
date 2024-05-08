using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraController : MonoBehaviour
{
    public Transform playerOne;
    public Transform playerTwo;

    public float minSizeY = 5f;
    public float cameraSpeed = 1.5f;
    public float maxZoom = 20.83f;
    public float minY = -1f;
    public float maxY = 5f;



    public FightManager fightManager;

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

        if (playerOne && playerTwo)
        {
            AdjustCameraPosition();
            AdjustCameraZoom();
        }
    }

    void AdjustCameraPosition()
    {
        Vector3 middlePoint = GetMiddlePoint();
        Vector3 cameraPosition = new Vector3(middlePoint.x, middlePoint.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, cameraPosition, cameraSpeed * Time.deltaTime);
    }


   void AdjustCameraZoom()
    {
        float distance = (playerOne.position - playerTwo.position).magnitude;
        float targetSize = Mathf.Clamp(distance, minSizeY, maxZoom);

        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, targetSize, cameraSpeed * Time.deltaTime);
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
