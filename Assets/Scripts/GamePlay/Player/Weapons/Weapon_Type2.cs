using UnityEngine;
using UnityEngine.UIElements;

public class Weapon_Type2 : Weapon_Base
{
    [SerializeField] GameObject[] variations;
    [SerializeField] Laser[] lasers;


    [SerializeField] float reduceRate = 0.5f;
    [SerializeField] float increaseRate = 0.2f;
    GameObject activeVariation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    public override void Active()
    {
        if (powerLevel < variations.Length)
        {
            for (int i = 0; i < variations.Length; i++)
            {
                if (variations[powerLevel] = variations[i])
                {
                    activeVariation = variations[i];
                }
                else
                    variations[i].SetActive(false);
            }
        }
        else
        {
            activeVariation = variations[variations.Length - 1];
        }

        if (currentBullets < maxBullets)
            currentBullets += increaseRate * Time.deltaTime;
        Fire();
    }

    void Fire()
    {
        lasers = activeVariation.GetComponentsInChildren<Laser>();
        if(inputController.isFiring && !isReloading && isActive)
        {
            activeVariation.SetActive(true);
            currentBullets -= reduceRate;
            foreach(Laser laser in lasers)
            {
                laser.isShooting = true;
            }
        }
        else
        {
            foreach (Laser laser in lasers)
                laser.isShooting = false;
        }
    }

    public void TurnOffLasers()
    {
        foreach(Laser laser in lasers)
        {
            laser.isShooting = false;
        }
        activeVariation.SetActive(false);
    }
}
