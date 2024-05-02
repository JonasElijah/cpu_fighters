using UnityEngine;
using UnityEngine.SceneManagement;

public class FightManager : MonoBehaviour
{
    public GameObject[] P1;
    public GameObject[] P2;

    public Transform LeftLedge;
    public Transform RightLedge;
    public Transform FallZone;

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
            SceneManager.LoadScene("EndScene");
        }
    }

    private void InstantiatePlayers()
    {
        GameObject playerOne = Instantiate(P1[CharacterSelectionHandler.playerOneCharacter], new Vector3(-5, 0, 0), Quaternion.identity);
        GameObject playerTwo = Instantiate(P2[CharacterSelectionHandler.playerTwoCharacter], new Vector3(5, 0, 0), Quaternion.identity);
        playerTwo.GetComponent<PlayerTwoInput>().isAI = CharacterSelectionHandler.playerTwoAI;
    }

}
