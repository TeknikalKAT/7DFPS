using System;
using UnityEngine;

public class Enemy_Health : MonoBehaviour
{
    [SerializeField] float maxHealth = 10f;
    [SerializeField] GameObject deathPrefab;
    [SerializeField] Transform homingTarget;        //for the magic (homing) projectile
    [SerializeField] float resetTime = 1f;
    [SerializeField] int maxScore = 4;
    [SerializeField] int minScore = 1;
    [SerializeField] float scoreRiser = 0.15f;
    float storeTime;
    float scoreTime;
    float currentHealth;
    bool hasBeenHit = false;
    bool scoreGotten = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0 && scoreGotten)
        {
            Die();
        }
    }
    public void DamageEnemy(float amount)
    {
        currentHealth -= amount;
        storeTime = scoreTime;
        if (!hasBeenHit)
            scoreTime = resetTime;
        else
        {
            scoreTime += scoreRiser;
            if (scoreTime >= resetTime)
                scoreTime = resetTime;
        }
        hasBeenHit = true;
        if (currentHealth <= 0 && !scoreGotten)
            ScorePoints();
    }

    void ScorePoints()
    {
        float pointAsFloat = minScore + ((maxScore - minScore) * (storeTime / resetTime));
        int score = Convert.ToInt32(pointAsFloat);

        ScoreManager.instance.AddPoints(score);
        scoreGotten = true;
    }
    void Die()
    {
        if(deathPrefab != null)
            Instantiate(deathPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public Transform HomingTarget()
    {
        return homingTarget;
    }
}
