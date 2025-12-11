using UnityEngine;

public class Weapon_Type3 : Weapon_Base
{
    //For SMG and Rifle (I think) kind of guns
    //Will be good for my cookie gunner, and for a sniper as well

    [SerializeField] float damageAmount;
    [SerializeField] Transform shootPoint;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject bulletDrops;

    [SerializeField] float fireRate = 1f;
    [SerializeField] float releaseRate = 0.1f;
    [SerializeField] float shootRange;
    [SerializeField] float spread = 0.2f;

    float _fireRate;
    float _releaseRate;
    RaycastHit hit;
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

        if (muzzleFlash != null)
            muzzleFlash.Play();

        if (bulletDrops != null)
        {
            GameObject drop = Instantiate(bulletDrops, shootPoint.position, shootPoint.rotation);
        }

        Vector3 direction = cam.transform.forward;
        direction.x += Random.Range(-spread, spread);
        direction.y += Random.Range(-spread, spread);
        if (Physics.Raycast(cam.transform.position, direction, out hit, shootRange))
        {
            if (hit.transform.GetComponent<Enemy_Health>())
            {
                hit.transform.GetComponent<Enemy_Health>().DamageEnemy(damageAmount);
            }
            //add hit particles
            Debug.Log(hit.transform.name);

        }
        _fireRate = fireRate;
        _releaseRate = releaseRate;
        currentBullets -= 1;

    }
}
