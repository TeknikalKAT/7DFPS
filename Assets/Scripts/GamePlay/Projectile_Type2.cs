using UnityEngine;

public class Projectile_Type2 : MonoBehaviour
{
    [SerializeField] float speed = 00f;
    [SerializeField] float lifeTime = 3f;
    [SerializeField] float damageAmount = 2f;

    Vector3 previousPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        previousPos = transform.position;
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 nextPos = transform.position + transform.forward * speed * Time.deltaTime;

        //find 'hit' between current position and previous position, something like that
        if(Physics.Raycast(previousPos, nextPos - previousPos, out RaycastHit hit, Vector3.Distance(previousPos, nextPos)))
        {
            if(hit.transform.GetComponent<Enemy_Health>())
            {
                hit.transform.GetComponent<Enemy_Health>().DamageEnemy(damageAmount);
            }
            DestroyProjectile();
            return;
        }

        transform.position = nextPos;

        previousPos = transform.position;
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);

    }
}
