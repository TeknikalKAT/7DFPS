using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float speed = 60f;
    [SerializeField] float lifeTime = 3f;
    [SerializeField] float damageAmount = 2f;
    [SerializeField] float treeDamageAmount = 0.5f;
    [SerializeField] bool isPlayer = true;


    [SerializeField] bool explode = false;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] float explodeRadius = 1f;
    [SerializeField] float explosionForce = 100f;
    [SerializeField] float upwardModifier = 1f;
    [SerializeField] GameObject explosionSound;
    [SerializeField] LayerMask layerToHit;

    [Header("Homing Properties")]
    [SerializeField] bool homingProjectile = false;
    [SerializeField] float homingRotateSpeed = 10f;
    [SerializeField] float homingDuration = 10f;        //for the enemies, if I'll consider them


    Rigidbody rb;
    Transform target;           //for homing missile
    bool gottenTarget = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * speed;
        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger)
        {
            if (homingProjectile)
            {
                if (isPlayer)
                {
                    if (other.GetComponent<Enemy_Health>() && !gottenTarget)
                    {
                        target = other.GetComponent<Enemy_Health>().HomingTarget();
                        gottenTarget = true;
                    }
                }
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.isTrigger)
        {
            if (isPlayer)
            {
                if (collision.collider.GetComponent<Enemy_Health>())
                {
                    collision.collider.GetComponent<Enemy_Health>().DamageEnemy(damageAmount);
                }
            }
            else
            {
                if (collision.collider.CompareTag("Player"))
                    GameObject.FindAnyObjectByType<HealthController>().DamagePlayer(damageAmount);

                if(collision.collider.CompareTag("Tree"))
                {
                    GameObject.FindAnyObjectByType<Tree_HealthController>().DamageTree(treeDamageAmount);
                }
            }
            DestroyProjectile();
        }
    }

    private void FixedUpdate()
    {
        if (homingProjectile)
        {
            rb.linearVelocity = transform.forward * speed;
            if (gottenTarget)
            {
                if (homingDuration > 0)
                {
                    homingDuration -= Time.deltaTime;


                    Vector3 direction = target.position - transform.position;

                    Quaternion targetRotation = Quaternion.LookRotation(direction);

                    rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * homingRotateSpeed));
                }
            }
        }
    }

    void DestroyProjectile()
    {
        if (explosionSound != null)
        {
            GameObject sound = Instantiate(explosionSound, transform.position, Quaternion.identity);
        }
        if (explode)
        {
            Collider[] objects = Physics.OverlapSphere(transform.position, explodeRadius, layerToHit, QueryTriggerInteraction.Ignore);

            foreach (Collider obj in objects)
            {
                
                if (isPlayer)
                {
                    if (obj.GetComponent<Enemy_Health>())
                    {
                        Rigidbody rB = obj.attachedRigidbody;
                        if (rb != null)
                        {
                            rb.AddExplosionForce(explosionForce, transform.position, explodeRadius, upwardModifier, ForceMode.Impulse);
                        }
                        obj.GetComponent<Enemy_Health>().DamageEnemy(damageAmount);
                    }
                }
                else
                {
                    if(obj.CompareTag("Player"))
                    {
                        GameObject.FindAnyObjectByType<HealthController>().DamagePlayer(damageAmount);
                    }
                }
            }
        }
        if (explosionPrefab != null)
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
