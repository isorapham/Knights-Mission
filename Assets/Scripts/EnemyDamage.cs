using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField]
    private int damageAmount = 1;
    [SerializeField]
    private bool isDestroyOnDamage;
    [SerializeField]
    private GameObject destroyEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealthController playerHealth = collision.gameObject.GetComponentInParent<PlayerHealthController>();
            DealDamage(playerHealth);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerHealthController playerHealth = collision.gameObject.GetComponentInParent<PlayerHealthController>();
            DealDamage(playerHealth);
        }
    }

    private void DealDamage(PlayerHealthController playerHealth)
    {
        if (playerHealth != null) //check null
        {
            playerHealth.DamagePlayer(damageAmount);

            if (isDestroyOnDamage)
            {
                if (destroyEffect != null)
                {
                    Instantiate(destroyEffect, transform.position, transform.rotation);
                }

                Destroy(this.gameObject);
            }
        }
    }
}
