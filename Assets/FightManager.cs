using UnityEngine;
using UnityEngine.SceneManagement;

public class FightManager : MonoBehaviour
{
    public GameObject[] characters;

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
        GameObject playerOne = Instantiate(characters[CharacterSelectionHandler.playerOneCharacter], new Vector3(-5, 0, 0), Quaternion.identity);
        GameObject playerTwo = Instantiate(characters[CharacterSelectionHandler.playerTwoCharacter], new Vector3(5, 0, 0), Quaternion.identity);
    }

}
