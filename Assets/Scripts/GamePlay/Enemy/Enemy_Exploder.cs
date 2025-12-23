using UnityEngine;

public class Enemy_Exploder : Enemy_Base
{

    [SerializeField] float damageAmount;
    [SerializeField] float explodeTime = 1.5f;
    [SerializeField] float explosionRadius = 1f;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] LayerMask layerToHit;
    [SerializeField] AudioClip[] explosionSounds;

    AudioSource audioSource;
    float _explodeTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        audioSource = GameObject.FindAnyObjectByType<Weapon_Manager>().GetComponent<AudioSource>();
        _explodeTime = explodeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!target)
            GetTarget();

        if(inRange)
        {
            _explodeTime -= Time.deltaTime;
            agent.speed = attackMoveSpeed;
            agent.updatePosition = false;
        }
        else
        {
            agent.speed = moveSpeed;
            agent.updatePosition = true;
        }
        MoveTowardsTarget();

        if (_explodeTime <= 0)
            Attack();
    }

    public override void Attack()
    {
        Collider[] objects = Physics.OverlapSphere(transform.position, explosionRadius, layerToHit, QueryTriggerInteraction.Ignore);
        
        foreach(Collider obj in objects)
        {
            if(obj.CompareTag("Player"))
            {
                GameObject.FindAnyObjectByType<HealthController>().DamagePlayer(damageAmount);
            }
        }
        Instantiate(explosionPrefab, transform.position, transform.rotation);
        if(explosionSounds != null)
            audioSource.PlayOneShot(explosionSounds[Random.Range(0, explosionSounds.Length)]);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            inRange = true;
        }
    }


}
