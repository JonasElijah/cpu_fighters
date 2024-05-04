using UnityEngine;

public static class CharacterSelectionHandler
{

    public static int playerOneCharacter;
    public static int playerTwoCharacter;
    public static bool playerTwoAI;
    public static float aiDifficulty = 1.0f;


    public static void playerOneSelect(int character)
    {
        playerOneCharacter = character; 
    }

    public static void playerTwoSelect(int character, bool ai)
    {
        playerTwoCharacter = character;
        playerTwoAI = ai;
    }
}
