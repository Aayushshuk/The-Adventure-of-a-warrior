using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeeleEnemy : MonoBehaviour
{

    [SerializeField] private float attackCooldown;
    [SerializeField] private int Damage;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerlayer;

    [SerializeField] private AudioClip attackSound;
    private float cooldownTimer;
    private Health playerHealth;

    private Animator animator;

    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (PlayerInSight())
        {
            if (cooldownTimer > attackCooldown && playerHealth.currentHealth > 0)
            {
                cooldownTimer = 0;
                animator.SetTrigger("meeleAttack");
                SoundManager.instance.PlaySound(attackSound);
            }
        }
        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }
      
    }
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range
            * transform.localScale.x * colliderDistance,new Vector3(boxCollider.bounds.size.x * range
            , boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0, 
        Vector2.left, 0,playerlayer);

        if(hit.collider != null)
            playerHealth = hit.collider.GetComponent<Health>();
        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range 
            * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range
            , boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            playerHealth.TakeDamage(Damage);
        }
    }
}
