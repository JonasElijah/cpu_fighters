using UnityEngine;
using UnityEngine.UI;

public class FillStatusBarPlayerOne : MonoBehaviour
{
    public PlayerOneInput playerOne;
    private Slider slider;

    void Start()
    {
        GameObject character = GameObject.FindGameObjectWithTag("PlayerOne");
        playerOne = character.GetComponent<PlayerOneInput>();
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value -= 0.005f;
    }
}
