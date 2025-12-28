using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [System.Serializable]
    public class HeartAnim
    {
        public float duration;
        public float scaleFactor;
        public Color pulseColor;
    }

    [SerializeField] float maxHealth = 100;
    [SerializeField] float increaseTime = 0.5f;
    [SerializeField] Text healthText;

    [Space]
    [SerializeField] SizeAnimation heartAnim;
    [SerializeField] ColorAnimation colorAnim;
    [SerializeField] HeartAnim[] curveSets;

    GameStatus gameStatus;
    float _increaseTime;
    WaveManager waveManager;
    float health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameStatus = GetComponent<GameStatus>();
        waveManager = GetComponent<WaveManager>();
        _increaseTime = increaseTime;
        if(PlayerPrefs.GetInt("Wave number") == 5)
        {
            maxHealth = 350;
        }
        else if(PlayerPrefs.GetInt("Wave number") == 10)
        {
            maxHealth = 750;
        }
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Die();
        }
        else
        {
            if(waveManager.waitingPeriod)
            {
                if(_increaseTime > 0)
                _increaseTime -= Time.deltaTime;
                else
                {
                    if ((health + 1) < maxHealth)
                        health += 1;
                    else
                        health = maxHealth;
                    _increaseTime = increaseTime;
                }
            }
        }
        healthText.text = health.ToString();
        HeartAnimation();
    }
    void Die()
    {
        if(!gameStatus.Over())
            gameStatus.GameOver();
    }
    public void DamagePlayer(float amount)
    {
        health -= amount;
    }

    void HeartAnimation()
    {
        if(health >= 0.75 * maxHealth)                            //100
        {
            heartAnim.duration = curveSets[0].duration;
            colorAnim.duration = curveSets[0].duration;

            colorAnim.pulseColor = curveSets[0].pulseColor;
            heartAnim.scaleFactor = curveSets[0].scaleFactor;
        }
        else if(health < 0.75 * maxHealth && health >= 0.50 * maxHealth)        //75
        {
            heartAnim.duration = curveSets[1].duration;
            colorAnim.duration = curveSets[1].duration;

            colorAnim.pulseColor = curveSets[1].pulseColor;
            heartAnim.scaleFactor = curveSets[1].scaleFactor;
        }
        else if(health < 0.50 * maxHealth && health >= 0.25 * maxHealth)        //50
        {
            heartAnim.duration = curveSets[2].duration;
            colorAnim.duration = curveSets[2].duration;

            colorAnim.pulseColor = curveSets[2].pulseColor;
            heartAnim.scaleFactor = curveSets[2].scaleFactor;
        }
        else if(health < 0.25 * maxHealth && health >= 0.10 * maxHealth)        //25
        {
            heartAnim.duration = curveSets[3].duration;
            colorAnim.duration = curveSets[3].duration;

            colorAnim.pulseColor = curveSets[3].pulseColor;
            heartAnim.scaleFactor = curveSets[3].scaleFactor;
        }
        else                                        //10
        {
            heartAnim.duration = curveSets[4].duration;
            colorAnim.duration = curveSets[4].duration;

            colorAnim.pulseColor = curveSets[4].pulseColor;
            heartAnim.scaleFactor = curveSets[4].scaleFactor;
        }
    }
}
