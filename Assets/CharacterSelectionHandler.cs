public static class CharacterSelectionHandler
{
    public static int playerOneCharacter;
    public static int playerTwoCharacter;

    public static void playerOneSelect(int character)
    {
        playerOneCharacter = character; 
    }

    public static void playerTwoSelect(int character)
    {
        playerTwoCharacter = character;
    }
}
