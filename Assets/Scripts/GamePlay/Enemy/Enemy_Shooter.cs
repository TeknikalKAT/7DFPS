using UnityEngine;
using UnityEngine.AI;

public class Enemy_Shooter : Enemy_Base
{
    [System.Serializable]
    public class Shooter
    {
        public Transform shootPoint;
        public float fireRate;
        public ParticleSystem shootFlash;
        [HideInInspector] public float _fireRate;
    }

    [Header("--Default--Attack--Style--")]
    public Shooter[] shooters;
    [SerializeField] float hoveringRadius = 50f;
    [SerializeField] float minAlertHoverDistance = 50f, maxAlertHoverDistance = 80f;
    [SerializeField] float minHoverTime = 3f, maxHoverTime = 5f;
    [SerializeField] float hoverRotateSpeed = 10f;
    [SerializeField] GameObject projectilePrefab;

    [Header("--FDV--Attack--Style--")]
    [SerializeField] bool fovAttacker;      //whether enemy attacks via FOV or not
    bool fovCanAttack;                      //bool that actually initiates the FOV attack sequence
    [SerializeField] Enemy_FieldOfView[] FOVs;

    float hoverDistance;
     float _hoverTime;
    bool isHovering;
    bool gottenPoint;

    [SerializeField] bool hoverTargetPlayer = true;
    Vector3 randomPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        hoverDistance = Random.Range(minAlertHoverDistance, maxAlertHoverDistance);
        base.Start();
        foreach (var shooter in shooters)
        {
            shooter._fireRate = shooter.fireRate;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!target)
            GetTarget();

        if(fovCanAttack)
        {
            foreach(var fov in FOVs)
            {
                fov.canShoot = true;
            }
        }
        else
        {
            foreach (var fov in FOVs)
                fov.canShoot = false;
        }
        if (inRange)
        {
            if (Vector3.Distance(transform.position, target.position) <= hoverDistance)
            {
                isHovering = true;
                if (_hoverTime > 0)
                {
                    _hoverTime -= Time.deltaTime;
                    if (!gottenPoint)
                        GetRandomPos();
                    agent.SetDestination(randomPos);
                }
                else
                {
                    gottenPoint = false;
                    _hoverTime = Random.Range(minHoverTime, maxHoverTime);
                }
                if (hoverTargetPlayer)
                    TargetPlayerOnHover();
                agent.speed = attackMoveSpeed;
            }
            else
            {
                isHovering = false;
                agent.updateRotation = true;
                _hoverTime = 0;
            }
            if (!fovAttacker)
                Attack();
            else
                fovCanAttack = true;

        }
        else
        {
            isHovering = false;
            agent.speed = moveSpeed;
            fovCanAttack = false;
        }
        if(!isHovering)
            MoveTowardsTarget();


    }
    Vector3 RandomNavMeshPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * hoveringRadius;
        randomDirection += transform.position;

        if(NavMesh.SamplePosition (randomDirection, out NavMeshHit hit, hoveringRadius, NavMesh.AllAreas))
        {
            return hit.position;
        }
        return transform.position;
    }
    void TargetPlayerOnHover()
    {
        agent.updateRotation = false;
        Vector3 lookDirection = target.position - transform.position;
        lookDirection.y = 0;
        if (lookDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookDirection), Time.deltaTime * hoverRotateSpeed);
        }
    }
    void GetRandomPos()
    {
        randomPos = RandomNavMeshPosition();
        gottenPoint = true;

    }
    public override void Attack()
    {
        foreach (var shooter in shooters)
        {
            shooter._fireRate -= Time.deltaTime;

            if (shooter._fireRate <= 0)
            {
                GameObject projectile = Instantiate(projectilePrefab, shooter.shootPoint.position, shooter.shootPoint.rotation);
                if (shooter.shootFlash != null)
                    shooter.shootFlash.Play();
                shooter._fireRate = shooter.fireRate;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!treeTarget)
        {
            if (other.CompareTag("Player"))
            {
                inRange = true;
            }
        }
        else
        {
            if (other.CompareTag("Tree"))
                inRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (!treeTarget)
        {
            if (other.CompareTag("Player"))
                inRange = false;
        }
        else
        {
            if (other.CompareTag("Tree"))
                inRange = false;
        }
    }


    public GameObject Projectile()  //used by the FOVs
    {
        return projectilePrefab;
    }
}
