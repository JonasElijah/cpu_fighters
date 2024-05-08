using UnityEngine;

public static class CharacterSelectionHandler
{

    public static int playerOneCharacter;
    public static int playerTwoCharacter;
    public static bool playerTwoAI;
    public static bool playerOneAI;

    public static float aiDifficultyp1 = 1.0f;
    public static float aiDifficultyp2 = 1.0f;


    public static void playerOneSelect(int character, bool ai)
    {
        playerOneCharacter = character; 
        playerOneAI = ai;
    }

    public static void playerTwoSelect(int character, bool ai)
    {
        playerTwoCharacter = character;
        playerTwoAI = ai;
    }
}
