using UnityEngine;

public class Rock : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground") 
        {
            Destroy(gameObject); 
        }

       if (collision.gameObject.CompareTag("PlayerOne") || collision.gameObject.CompareTag("PlayerTwo")) 
        {
            PlayerInput playerInput = collision.gameObject.GetComponent<PlayerInput>();
            if (playerInput != null)
            {
                playerInput.TakeDamage(0.5f);
            }
            else
            {
                Debug.LogError("PlayerInput script not found on the collided player");
            }

            Destroy(gameObject);
        }
    }
}
