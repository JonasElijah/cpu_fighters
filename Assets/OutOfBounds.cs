using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "PlayerTwo")
        {
            other.GetComponent<PlayerTwoInput>().TakeDamage(999.9f);
        }

        if (other.gameObject.tag == "PlayerOne")
        {
            other.GetComponent<PlayerOneInput>().TakeDamage(999.9f);
        }
    }
}
