using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    [SerializeField] float damageAmount;
    [SerializeField] bool playerInRange;
    [SerializeField] AudioClip[] hitSounds;
    [SerializeField] Enemy_Attacker attackController;
    AudioSource audioSource;
    Animator anim;
    bool isAttacking;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GameObject.FindAnyObjectByType<Weapon_Manager>().GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange)
        {
            attackController.NoMove();
            if (!isAttacking)
            {
                anim.SetTrigger("Attack");
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
            if(hitSounds != null)
                audioSource.PlayOneShot(hitSounds[Random.Range(0, hitSounds.Length)]);
            GameObject.FindAnyObjectByType<HealthController>().DamagePlayer(damageAmount);
        }
        isAttacking = false;
    }
}
