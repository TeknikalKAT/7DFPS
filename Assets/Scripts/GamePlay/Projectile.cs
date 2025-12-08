using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] float damageAmount;
    [SerializeField] bool rayCaster;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float lifeTime = 10f;
    [SerializeField] LayerMask layerMask;
    [SerializeField] bool isPlayer;
    float distance = 0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, lifeTime);  
    }

    // Update is called once per frame
    void Update()
    {
        distance += moveSpeed * Time.deltaTime;
        RaycastHit hit;
        if (rayCaster)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, distance, layerMask, QueryTriggerInteraction.Ignore))
            {
                if (isPlayer)
                {
                    if (hit.transform.GetComponent<Enemy_Health>())
                    {
                        hit.transform.GetComponent<Enemy_Health>().DamageEnemy(damageAmount);
                    }
                }
                else
                {
                    if (hit.transform.CompareTag("Player"))
                    {
                        GameObject.FindAnyObjectByType<HealthController>().DamagePlayer(damageAmount);
                        Debug.Log("Hit Player!");
                    }
                }
                Destroy(gameObject);
            }
        }
        else
            transform.Translate(Vector3.forward * distance);
    }

    private void OnTriggerEnter(Collider other)
    {
        //for the child object
        if(!rayCaster)
        {
            Debug.Log(other.name);
            Destroy(transform.parent);
        }
    }
}
