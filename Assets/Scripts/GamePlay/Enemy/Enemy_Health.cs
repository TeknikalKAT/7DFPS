using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    [SerializeField] float maxHealth = 10f;

    float currentHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            Die();
        }
    }
    public void DamageEnemy(float amount)
    {
        currentHealth -= amount;
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
