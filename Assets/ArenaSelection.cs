using UnityEngine;
using UnityEngine.SceneManagement;

public class ArenaSelection : MonoBehaviour
{

    public void Forest()
    {
        SceneManager.LoadScene("DefaultArena");
    }

    public void Desert()
    {
        SceneManager.LoadScene("DesertScene");
    }

    public void City()
    {
        SceneManager.LoadScene("CityScene");
    }
}
