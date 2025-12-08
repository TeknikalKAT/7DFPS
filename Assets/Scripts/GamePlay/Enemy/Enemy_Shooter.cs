using UnityEngine;

public class Enemy_Shooter : Enemy_Base
{
    [System.Serializable]
    public class Shooter
    {
        public Transform shootPoint;
        public float fireRate;
        [HideInInspector] public float _fireRate;
    }

    public Shooter[] shooters;
    [SerializeField] float minStopDistance, maxStopDistance;
    [SerializeField] GameObject projectilePrefab;

    float stopDistance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        stopDistance = Random.Range(minStopDistance, maxStopDistance);
        base.Start();
        foreach (var shooter in shooters)
        {
            shooter._fireRate = shooter.fireRate;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(inRange)
        {
            if (Vector3.Distance(transform.position, target.position) <= stopDistance)
                agent.speed = 0f;
            else
                agent.speed = attackMoveSpeed;
            Attack();
        }
        else
        {
            agent.speed = moveSpeed;
        }
        MoveTowardsTarget();
    }
    public override void Attack()
    {
        foreach (var shooter in shooters)
        {
            shooter._fireRate -= Time.deltaTime;

            if(shooter._fireRate <= 0)
            {
                GameObject projectile = Instantiate(projectilePrefab, shooter.shootPoint.position, shooter.shootPoint.rotation);
                shooter._fireRate = shooter.fireRate;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            inRange = false;
    }
}
