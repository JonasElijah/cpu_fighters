using UnityEngine;
using UnityEngine.UI;

public class FillStatusBarPlayerTwo : MonoBehaviour
{
    public PlayerTwoInput playerTwo;
    private Slider slider;
    private float health;
    private GameObject fillArea;

    void Awake()
    {
        health = playerTwo.currentHealth;
        slider = GetComponent<Slider>();
 
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = health;
    }
}
