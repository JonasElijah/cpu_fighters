using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public void Start()
    {
        CharacterSelectionHandler.playerOneCharacter = -1;
        CharacterSelectionHandler.playerTwoCharacter = -1;
    }
    
    public void Play()
    {
        SceneManager.LoadScene("CharacterSelect");
    }

    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
