using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameHandler : MonoBehaviour
{
     public void PlayAgain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
