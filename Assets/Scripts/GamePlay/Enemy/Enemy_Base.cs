using UnityEngine;
using UnityEngine.AI;

public class Enemy_Base : MonoBehaviour
{

    [SerializeField] bool treeTarget;
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
            target = GameObject.Find("Tree").transform;
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
