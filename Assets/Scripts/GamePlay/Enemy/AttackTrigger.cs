using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    [SerializeField] float damageAmount;
    [SerializeField] bool playerInRange;
    [SerializeField] Enemy_Attacker attackController;
    Animator anim;
    bool isAttacking;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange)
        {
            if(!isAttacking)
            {
                anim.SetTrigger("Attack");
                attackController.NoMove();
                isAttacking = true;
            }
        }
        else
        {
            attackController.Move();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    public void Attack()
    {
        if(playerInRange)
        {
            GameObject.FindAnyObjectByType<HealthController>().DamagePlayer(damageAmount);
        }
        isAttacking = false;
    }
}
