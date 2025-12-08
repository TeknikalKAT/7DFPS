using UnityEngine;

public class Weapon_Type1 : Weapon_Base
{
    [SerializeField] GameObject[] projectiles;
    [SerializeField] Transform shootPoint;      //I may make this an array in the future

    [Header("Normal Rate Props")]
    [SerializeField] float fireRate = 1f;

    [Header("Rapid Rate Props")]
    [SerializeField] float releaseRate = 0.1f;

    float _fireRate;
    float _releaseRate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        _fireRate = 0;
    }

    public override void Active()
    {
        if (_fireRate > 0)
        {
            _fireRate -= Time.deltaTime;
        }
        else
            Fire();

        RapidShooting();
    }
    void RapidShooting()
    {
        if (!inputController.isFiring)
            _releaseRate -= Time.deltaTime;

        if(_releaseRate <= 0)
        {
            _releaseRate = 0;
            Fire();
        }
    }

    void Fire()
    {
        if(inputController.isFiring && !isReloading && isActive)
        {
            _fireRate = fireRate;
            _releaseRate = releaseRate;

            currentBullets -= 1;
            
            if(powerLevel < projectiles.Length)
            {
                GameObject projectile = Instantiate(projectiles[powerLevel], shootPoint.position, shootPoint.rotation);
            }
            else
            {
                GameObject projectile = Instantiate(projectiles[projectiles.Length - 1], shootPoint.position, shootPoint.rotation);
            }
        }
    }

}
