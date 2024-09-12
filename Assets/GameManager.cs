public class GameManager
{
    public static GameManager instance;

    public float playerOneHealth = 10;
    public float playerTwoHealth = 10;

    private GameManager() { }

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }

    public bool checkGame()
    {
        return playerOneHealth <= 0 || playerTwoHealth <= 0;
    }

    public void endGame()
    {
        SceneManager.LoadScene("EndScene");
    }

    public void setPlayerOneHealth(float x)
    {
        playerOneHealth = x;
    }

    public void setPlayerTwoHealth(float x)
    {
        playerTwoHealth = x;
    }
}
