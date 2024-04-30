using UnityEngine;
public class Health : MonoBehaviour
{
    public int maxHealth = 10;
    public int currentHealth;
    // Start is called before the first frame update
    public void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if(currentHealth <= 0 )
        {
            //Die
            //Play Death Animation
            //Transition to death screen   
        }
    }

    public int getHealth()
    {
        return currentHealth;
    }


}
