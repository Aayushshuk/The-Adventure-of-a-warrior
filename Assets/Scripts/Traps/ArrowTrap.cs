using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{

    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] Arrows;
    private float cooldownTimer = Mathf.Infinity;

    private void Attack()
    {
        cooldownTimer = 0;
        Arrows[FindArrows()].transform.position = firePoint.position;
        Arrows[FindArrows()].GetComponent<EnemyProjectiles>().ActivateProjectile();
    }
    private int FindArrows()
    {
        for (int i = 0; i < Arrows.Length; i++)
        {
            if (!Arrows[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }

    private void Update()
    {
        if (cooldownTimer >= attackCooldown)
        {
            Attack();
        }
    }
}
