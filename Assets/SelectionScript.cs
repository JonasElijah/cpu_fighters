using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    
    }

    // public void next()
    // {
    //     characters[selectedCharacter].SetActive(false);
    //     selectedCharacter = (selectedCharacter + 1 ) % characters.Length;
    //     characters[selectedCharacter].SetActive(true);
    // }

    // public void prev()
    // {
    //     characters[selectedCharacter].SetActive(false);
    //     selectedCharacter--;
    //     if(selectedCharacter < 0 )
    //     {
    //         selectedCharacter += characters.Length;
    //     }
    //     characters[selectedCharacter].SetActive(true);
    // }

    public void charOneSel(int player)
    {
        CharacterSelectionHandler.playerOneSelect(player);
    }

    public void charTwoSel(int player)
    {
        CharacterSelectionHandler.playerTwoSelect(player);
    }

    public void Fight()
    {
        SceneManager.LoadScene("DefaultArena");
    }
}
