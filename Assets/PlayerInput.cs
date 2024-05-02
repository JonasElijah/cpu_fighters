using UnityEngine;
using UnityEngine.UI;

public abstract class PlayerInput : MonoBehaviour
{
    public GameObject character;
    public GameObject healthBar;
    public Fighter fighter;
    public Slider slider;
    public SpriteRenderer spriteRenderer;
    public Color originalColor;
    public GameObject playerIndicator;

    public abstract void TakeDamage(float amount);



}
