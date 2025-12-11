using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float speed = 60f;
    [SerializeField] float lifeTime = 3f;
    [SerializeField] float damageAmount = 2f;
    [SerializeField] bool isPlayer = true;
    [SerializeField] bool explode = false;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] float explodeRadius = 1f;
    [SerializeField] LayerMask layerToHit;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.linearVelocity = transform.forward * speed;

        Destroy(gameObject, lifeTime);
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
            }
            DestroyProjectile();
        }
    }

    void DestroyProjectile()
    {
        if(explode)
        {
            Collider[] objects = Physics.OverlapSphere(transform.position, explodeRadius, layerToHit, QueryTriggerInteraction.Ignore);

            foreach (Collider obj in objects)
            {
                if (isPlayer)
                {
                    if (obj.GetComponent<Enemy_Health>())
                    {
                        Debug.Log(obj.name);
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
