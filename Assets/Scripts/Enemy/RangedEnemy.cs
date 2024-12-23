using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Ranged Attack")]
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireBalls;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [Header("FireBall Sound")]
    [SerializeField] private AudioClip fireBallSound;
    private Animator animator;
    private Health playerHealth;
    private EnemyPatrol EnemyPatrol;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerHealth = GetComponent<Health>();
        EnemyPatrol = GetComponent<EnemyPatrol>();
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                animator.SetTrigger("rangedAttack");
            }
        }
        if (EnemyPatrol != null)
        {
            EnemyPatrol.enabled = !PlayerInSight();
        }
    }
    private void RangedAttack()
    {
        SoundManager.instance.PlaySound(fireBallSound);
        cooldownTimer = 0;
        //Shoot
        fireBalls[FindFireBalls()].transform.position = firepoint.position;
        fireBalls[FindFireBalls()].GetComponent<EnemyProjectiles>().ActivateProjectile();
    }

    private int FindFireBalls()
    {
        for (int i = 0; i < fireBalls.Length; i++)
        {
            if (!fireBalls[i].activeInHierarchy)
            {
                return i;
            }
            
        }
        return 0;
    }
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range
            * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range
            , boxCollider.bounds.size.y, boxCollider.bounds.size.z), 0,
        Vector2.left, 0, playerLayer);

     
        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range
            * transform.localScale.x * colliderDistance, new Vector3(boxCollider.bounds.size.x * range
            , boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
}
