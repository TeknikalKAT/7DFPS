using UnityEngine;
using UnityEngine.AI;

public class Enemy_Base : MonoBehaviour
{

    public int weight = 1;
    public int minWaveNumber = 1;

    [SerializeField] protected bool treeTarget;
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected float rotateSpeed = 10f;
    [SerializeField] protected Transform rotatePoint;

    [SerializeField] protected float attackMoveSpeed = 0f;
    protected Transform target;
    public bool inRange;
    protected NavMeshAgent agent;

    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        inRange = false;
        GetTarget();
    }

    protected void GetTarget()
    {
        if (treeTarget)
        {
            GameObject[] transforms = GameObject.FindGameObjectsWithTag("TreeTarget");
            int random = Random.Range(0, transforms.Length);
            target = transforms[random].transform;
            Debug.Log(target.name);
        }
        else
            target = GameObject.FindWithTag("Player").transform;
    }

    protected void MoveTowardsTarget()
    {
        agent.SetDestination(target.position);
    }

    public virtual void Attack()
    {
        Debug.Log("Attack");
    }
}
