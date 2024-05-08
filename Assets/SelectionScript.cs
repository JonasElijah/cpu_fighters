using System.Collections;
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

    public GameObject aiButton1;
    public GameObject aiButton2;
    public GameObject aiDifficulty1;
    public GameObject aiDifficulty2;
    public GameObject Back;
    public GameObject selectArena;

    public bool aiValp2 = false;
    public bool aiValp1 = false;


    public TMP_Dropdown dropdown;

    // Start is called before the first frame update
    void Start()
    {
        CharacterSelectionHandler.aiDifficultyp1 = 1.0f;
        CharacterSelectionHandler.aiDifficultyp2 = 1.0f;
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
        aiButton1.GetComponent<AudioSource>().Play();
        P1[selectedCharacterP1].SetActive(false);
        selectedCharacterP1 = (selectedCharacterP1 + 1 ) % P1.Length;
        P1[selectedCharacterP1].SetActive(true);
        UpdateCharacterImage(playerOne);
    }

    public void prevP1()
    {
        aiButton1.GetComponent<AudioSource>().Play();
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
        aiButton1.GetComponent<AudioSource>().Play();
        P2[selectedCharacterP2].SetActive(false);
        selectedCharacterP2 = (selectedCharacterP2 + 1 ) % P1.Length;
        P2[selectedCharacterP2].SetActive(true);
        UpdateCharacterImage(playerTwo);
    }

    public void prevP2()
    {
        aiButton1.GetComponent<AudioSource>().Play();
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
        CharacterSelectionHandler.playerOneSelect(player, aiValp1);
    }

    public void charTwoSel(int player)
    {
        CharacterSelectionHandler.playerTwoSelect(player,aiValp2);
    }

    public void SetAIP2()
    {
        aiButton2.GetComponent<AudioSource>().Play();
        aiValp2 = true;
    }

    public void SetAIP1()
    {
        aiButton1.GetComponent<AudioSource>().Play();
        aiValp1 = true;
    }

    public void Fight()
    {
        CharacterSelectionHandler.playerOneSelect(selectedCharacterP1,aiValp1);
        CharacterSelectionHandler.playerTwoSelect(selectedCharacterP2,aiValp2);
        StartCoroutine(PlaySoundAndLoadScene(Back,"ArenaSelection"));   
    }

    public void DifficultyChangedP2()
    {
        aiDifficulty2.GetComponent<AudioSource>().Play();
        switch (dropdown.value)
        {
            case 0:
                Debug.Log("Easy difficulty selected");
                CharacterSelectionHandler.aiDifficultyp2 = 1.0f;
                break;
            case 1:
                Debug.Log("Medium difficulty selected");
                CharacterSelectionHandler.aiDifficultyp2 = 0.2f;
                break;
            case 2:
                Debug.Log("Hard difficulty selected");
                CharacterSelectionHandler.aiDifficultyp2 = 0.005f;
                break;
        }
    }

     public void DifficultyChangedP1()
    {
        aiDifficulty1.GetComponent<AudioSource>().Play();
        switch (dropdown.value)
        {
            case 0:
                Debug.Log("Easy difficulty selected");
                CharacterSelectionHandler.aiDifficultyp1 = 1.0f;
                break;
            case 1:
                Debug.Log("Medium difficulty selected");
                CharacterSelectionHandler.aiDifficultyp1 = 0.2f;
                break;
            case 2:
                Debug.Log("Hard difficulty selected");
                CharacterSelectionHandler.aiDifficultyp1 = 0.005f;
                break;
        }
    }

    // public void back()
    // {
    //     SceneManager.LoadScene("MainMenu");
    // }

    public void back()
    {
        StartCoroutine(PlaySoundAndLoadScene(Back,"MainMenu"));
    }

    IEnumerator PlaySoundAndLoadScene(GameObject obj, string sceneName)
    {
        AudioSource audio = obj.GetComponent<AudioSource>();
        audio.Play();
        yield return new WaitForSeconds(audio.clip.length); 
        SceneManager.LoadScene(sceneName);
    }

}
