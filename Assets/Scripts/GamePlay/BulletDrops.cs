using UnityEngine;

public class BulletDrops : MonoBehaviour
{
    [SerializeField] float force = 5f;
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.right * Random.Range(-force, force));
    }

}
