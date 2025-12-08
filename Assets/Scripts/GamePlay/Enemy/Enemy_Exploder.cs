using UnityEngine;

public class Enemy_Exploder : Enemy_Base
{

    [SerializeField] float damageAmount;
    [SerializeField] float explodeTime = 1.5f;
    [SerializeField] float explosionRadius = 1f;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] LayerMask layerToHit;

    float _explodeTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
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
        }
        else
        {
            agent.speed = moveSpeed;
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
