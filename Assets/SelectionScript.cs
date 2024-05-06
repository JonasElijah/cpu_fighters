using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectionScript : MonoBehaviour
{
    public GameObject[] P1;
    public GameObject[] P2;

    public int selectedCharacterP1 = 0;
    public int selectedCharacterP2 = 0;

    public GameObject playerOne;
    public GameObject playerTwo;

    public bool aiVal = false;

    public TMP_Dropdown dropdown;

    // Start is called before the first frame update
    void Start()
    {
        P1[selectedCharacterP1].SetActive(true);
        P2[selectedCharacterP2].SetActive(true);
 
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateCharacterImage(GameObject character) 
    {
        SpriteRenderer sr = P1[selectedCharacterP1].GetComponent<SpriteRenderer>();
        if (sr != null) 
        {
            character.GetComponent<SpriteRenderer>().sprite = sr.sprite;

        }
    }

    public void nextP1()
    {
        P1[selectedCharacterP1].SetActive(false);
        selectedCharacterP1 = (selectedCharacterP1 + 1 ) % P1.Length;
        P1[selectedCharacterP1].SetActive(true);
        UpdateCharacterImage(playerOne);
    }

    public void prevP1()
    {
        P1[selectedCharacterP1].SetActive(false);
        selectedCharacterP1--;
        if(selectedCharacterP1 < 0 )
        {
            selectedCharacterP1 += P1.Length;
        }
        P1[selectedCharacterP1].SetActive(true);
        UpdateCharacterImage(playerOne);
    }

    public void nextP2()
    {
        P2[selectedCharacterP2].SetActive(false);
        selectedCharacterP2 = (selectedCharacterP2 + 1 ) % P1.Length;
        P2[selectedCharacterP2].SetActive(true);
        UpdateCharacterImage(playerTwo);
    }

    public void prevP2()
    {
        P2[selectedCharacterP2].SetActive(false);
        selectedCharacterP2--;
        if(selectedCharacterP2 < 0 )
        {
            selectedCharacterP2 += P2.Length;
        }
        P2[selectedCharacterP2].SetActive(true);
        UpdateCharacterImage(playerTwo);
    }

    public void charOneSel(int player)
    {
        CharacterSelectionHandler.playerOneSelect(player);
    }

    public void charTwoSel(int player)
    {
        CharacterSelectionHandler.playerTwoSelect(player,aiVal);
    }

    public void SetAI()
    {
        aiVal = true;
    }

    public void Fight()
    {
        CharacterSelectionHandler.playerOneSelect(selectedCharacterP1);
        CharacterSelectionHandler.playerTwoSelect(selectedCharacterP2,aiVal);
        SceneManager.LoadScene("ArenaSelection");
    }

    public void DifficultyChanged()
    {
        switch (dropdown.value)
        {
            case 0:
                Debug.Log("Easy difficulty selected");
                CharacterSelectionHandler.aiDifficulty = 1.0f;
                break;
            case 1:
                Debug.Log("Medium difficulty selected");
                CharacterSelectionHandler.aiDifficulty = 0.2f;
                break;
            case 2:
                Debug.Log("Hard difficulty selected");
                CharacterSelectionHandler.aiDifficulty = 0.005f;
                break;
        }
    }

    public void back()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
