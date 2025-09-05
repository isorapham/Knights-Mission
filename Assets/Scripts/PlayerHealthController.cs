using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField]
    private int maxHealth;
    [SerializeField]
    private int currentHealth;

    [SerializeField]
    private float invicibilityTime;
    private float invicCounter;

   
    Animator animator;
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (invicCounter > 0)
        {
            invicCounter -= Time.deltaTime;       
        }
    }

    public void HealPlayer(int healthAmount)
    {
        currentHealth += healthAmount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

 
    }

    public void DamagePlayer(int damageAmount)
    {
        if (invicCounter <= 0)
        {
            currentHealth -= damageAmount;
 
            if (currentHealth <= 0)
            {
                // Thua Game
                animator.SetBool(AnimationStrings.isAlive, false);
            }
            else
            {
                invicCounter = invicibilityTime;
            }
        }
    }

    private void SetMaxHealth()
    {
        currentHealth = maxHealth;
    }
}

