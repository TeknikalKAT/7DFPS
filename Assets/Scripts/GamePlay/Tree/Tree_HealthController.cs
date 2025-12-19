using UnityEngine;
using UnityEngine.UI;

public class Tree_HealthController : MonoBehaviour
{

    [SerializeField] float maxHealth;
    [SerializeField] Slider healthSlider;
    [SerializeField] float increaseRate;

    GameStatus gameStatus;
    WaveManager waveManager;
    float health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameStatus = GameObject.FindAnyObjectByType<GameStatus>();
        waveManager = GameObject.FindAnyObjectByType<WaveManager>();
        health = maxHealth;
        healthSlider.maxValue = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
            GameOver();

        if (waveManager.waitingPeriod)
        {
            if (health < maxHealth)
            {
                health += Time.deltaTime * increaseRate;
            }
            else
                health = maxHealth;

        }
        healthSlider.value = health;
    }

    public void DamageTree(float amount)
    {
        health -= amount;
    }

    void GameOver()
    {
        health = 0;
        if(!gameStatus.Over())
            gameStatus.GameOver();
    }
}
