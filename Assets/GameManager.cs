using UnityEngine.SceneManagement;

public static class GameManager 
{
    public static float playerOneHealth = 10;
    public static float playerTwoHealth = 10;

    public static bool checkGame()
    {
        return playerOneHealth <= 0 || playerTwoHealth <= 0;
    }

    public static void endGame()
    {
        SceneManager.LoadScene("EndScene");
    }

    public static void setPlayerOneHealth(float x)
    {
        playerOneHealth = x;
    }

    public static void setPlayerTwoHealth(float x)
    {
        playerTwoHealth = x;
    }
}
