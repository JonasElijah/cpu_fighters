using UnityEngine;
using UnityEngine.SceneManagement;

public class FightManager : MonoBehaviour
{
    public GameObject[] P1;
    public GameObject[] P2;

    public Transform LeftLedge;
    public Transform RightLedge;
    public Transform FallZone;

    public GameObject playerOne;
    public GameObject playerTwo;

    public GameObject MovingPlatform;

    void Start()
    {
        GameManager.playerOneHealth = 10;
        GameManager.playerTwoHealth = 10;
        InstantiatePlayers();
    }

    void Update()
    {
        if (GameManager.checkGame())
        {
            if(MovingPlatform)
                MovingPlatform.SetActive(false);
            SceneManager.LoadScene("EndScene");
        }
    }

    private void InstantiatePlayers()
    {
        playerOne = Instantiate(P1[CharacterSelectionHandler.playerOneCharacter], new Vector3(-5, 0, 0), Quaternion.identity);
        playerTwo = Instantiate(P2[CharacterSelectionHandler.playerTwoCharacter], new Vector3(5, 0, 0), Quaternion.identity);
        
        playerOne.GetComponent<PlayerOneInput>().isAI = CharacterSelectionHandler.playerOneAI;
        playerTwo.GetComponent<PlayerTwoInput>().isAI = CharacterSelectionHandler.playerTwoAI;
    }

}
