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
    [SerializeField] Text healthText;

    [Space]
    [SerializeField] SizeAnimation heartAnim;
    [SerializeField] ColorAnimation colorAnim;
    [SerializeField] HeartAnim[] curveSets;

    GameStatus gameStatus;
    float health;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameStatus = GetComponent<GameStatus>();
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Die();
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
        if(health >= 75)                            //100
        {
            heartAnim.duration = curveSets[0].duration;
            colorAnim.duration = curveSets[0].duration;

            colorAnim.pulseColor = curveSets[0].pulseColor;
            heartAnim.scaleFactor = curveSets[0].scaleFactor;
        }
        else if(health < 75 && health >= 50)        //75
        {
            heartAnim.duration = curveSets[1].duration;
            colorAnim.duration = curveSets[1].duration;

            colorAnim.pulseColor = curveSets[1].pulseColor;
            heartAnim.scaleFactor = curveSets[1].scaleFactor;
        }
        else if(health < 50 && health >= 25)        //50
        {
            heartAnim.duration = curveSets[2].duration;
            colorAnim.duration = curveSets[2].duration;

            colorAnim.pulseColor = curveSets[2].pulseColor;
            heartAnim.scaleFactor = curveSets[2].scaleFactor;
        }
        else if(health < 25 && health >= 10)        //25
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
