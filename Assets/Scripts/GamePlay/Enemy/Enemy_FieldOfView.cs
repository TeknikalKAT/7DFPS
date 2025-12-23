using UnityEngine;

public class Enemy_FieldOfView : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] bool startTarget = true;
    [SerializeField] bool tree = false;
    [SerializeField] GameObject fovStartPoint;
    [SerializeField] float rotateSpeed = 200f;
    [SerializeField] float maxAngle = 90f;
    [SerializeField] float maxAngleResetPoint = 90f;

    [Header("FOV ATTACKING")]
    [SerializeField] bool shooter = true;
    bool _canShoot = false;
    public bool canShoot
    {
        get { return _canShoot; }
        set { _canShoot = value; }
    }
    [SerializeField] Transform shootPoint;
    [SerializeField] float shootTime;
    [SerializeField] ParticleSystem shootFlash;
    float _shootTime;
    Enemy_Shooter enemyShooter;
    GameObject projectile;

    [Space]
    [SerializeField] bool xRotation = true;
    [SerializeField] bool yRotation = true;
    [SerializeField] bool zRotation = false;

    
    Quaternion initialRotation;

    Quaternion targetRotation;
    Quaternion lookAt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (shooter)
        {
            enemyShooter = GetComponentInParent<Enemy_Shooter>();
            _shootTime = shootTime;
            projectile = enemyShooter.Projectile();
        }
        if (tree)
            target = GameObject.FindWithTag("Tree").transform;
        else
            target = GameObject.FindWithTag("Player").transform;
        initialRotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTarget)
        {
            if (InFieldOfView(fovStartPoint))
            {
                Vector3 direction = target.position - transform.position;
                direction = new Vector3(xRotation ? direction.x : 0f, yRotation ? direction.y : 0f, zRotation ? direction.z : 0f);


                targetRotation = Quaternion.LookRotation(direction);

                lookAt = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
                transform.rotation = lookAt;

                if(shooter && canShoot)
                    Fire();

            }
            else if (InFieldOfViewNoResetPoint(fovStartPoint))
            {
                return;
            }
            else
            {
                targetRotation = initialRotation;
                transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, Time.deltaTime * rotateSpeed);
                _shootTime = shootTime;

            }
        }
        else
        {
            targetRotation = initialRotation;
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, targetRotation, Time.deltaTime * rotateSpeed);
        }
    }

    bool InFieldOfView(GameObject looker)
    {
        Vector3 targetDir = target.position - transform.position;
        float angle= Vector3.Angle(targetDir,looker.transform.forward);

        if (angle < maxAngle)
        {
            return true;
        }
        else
            return false;
    }

    bool InFieldOfViewNoResetPoint(GameObject looker)
    {
        Vector3 targetDir = target.position - transform.position;
        float angle = Vector3.Angle(targetDir, looker.transform.forward);

        if (angle < maxAngleResetPoint)
        {
            return true;
        }
        else
            return false;
    }

    public void AllowTarget(bool allow)
    {
        startTarget = allow;
    }

    void Fire()
    {
        _shootTime -= Time.deltaTime;
        if(_shootTime <= 0)
        {
            Instantiate(projectile, shootPoint.position, shootPoint.rotation);
            if (shootFlash != null)
                shootFlash.Play();
            _shootTime = shootTime;
        }
    }
}
