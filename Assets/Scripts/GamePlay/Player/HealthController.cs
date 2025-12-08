using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{

    [SerializeField] float maxHealth = 100;

    [SerializeField] Text healthText;
    float health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Die();
        }
        healthText.text = health.ToString() + "%";
    }
    void Die()
    {
        Debug.Log("Game Over");
    }
    public void DamagePlayer(float amount)
    {
        health -= amount;
    }
}
