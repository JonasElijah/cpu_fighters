using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody2D rb;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ground") 
        {
            Destroy(gameObject); 
        }
        
        if(other.gameObject.tag == "PlayerTwo")
        {
            Debug.Log("HIHIHIHIHIASDFLADJFKLDFJLDF");
            other.GetComponent<PlayerTwoInput>().TakeDamage(0.1f);
            Destroy(this.gameObject);
        }

        if(other.gameObject.tag == "PlayerOne")
        {
            other.GetComponent<PlayerOneInput>().TakeDamage(0.1f);
            Destroy(this.gameObject);
        }
    }


}




