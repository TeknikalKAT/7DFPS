using UnityEngine;

public class Enemy_TreeAttacker : Enemy_Base
{
    [SerializeField] float damageAmount;
    [SerializeField] float damageTime;
    [SerializeField] Enemy_FieldOfView[] fovs;

    float _damageTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
        _damageTime = damageTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!target)
            GetTarget();

        if(inRange)
        {
            _damageTime -= Time.deltaTime;
            agent.speed = attackMoveSpeed;

            if(fovs != null)
            {
                foreach(var fov in fovs)
                {
                    fov.AllowTarget(true);
                }
            }
        }
        else
        {

            agent.speed = moveSpeed;
            if (fovs != null)
            {
                foreach (var fov in fovs)
                {
                    fov.AllowTarget(false);
                }
            }
        }

        MoveTowardsTarget();

        if (_damageTime <= 0)
            Attack();
    }

    public override void Attack()
    {
        target.GetComponent<Tree_HealthController>().DamageTree(damageAmount);
        _damageTime = damageTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tree"))
            inRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Tree"))
            inRange = false;
    }
}
