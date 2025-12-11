using UnityEngine;

public class Weapon_Type1 : Weapon_Base
{
    //good for a snow ball cannon, or a milk shooter

    [SerializeField] GameObject[] projectiles;
    [SerializeField] Transform shootPoint;      //I may make this an array in the future
    [SerializeField] ParticleSystem shootParticles;
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
        {
            if (inputController.isFiring && !isReloading && isActive)
                anim.CrossFadeInFixedTime("Shoot", 0.01f);
        }

        RapidShooting();
    }
    void RapidShooting()
    {
        if (!inputController.isFiring)
            _releaseRate -= Time.deltaTime;

        if(_releaseRate <= 0)
        {
            _releaseRate = 0;
            if(inputController.isFiring && !isReloading && isActive)
                anim.CrossFadeInFixedTime("Shoot", 0.01f);
        }
    }

    public void Fire()
    {
        currentBullets -= 1;
        if (shootParticles != null)
            shootParticles.Play();
        if (powerLevel < projectiles.Length)
        {
            GameObject projectile = Instantiate(projectiles[powerLevel], shootPoint.position, shootPoint.rotation);
        }
        else
        {
            GameObject projectile = Instantiate(projectiles[projectiles.Length - 1], shootPoint.position, shootPoint.rotation);
        }
        _fireRate = fireRate;
        _releaseRate = releaseRate;

    }

}
