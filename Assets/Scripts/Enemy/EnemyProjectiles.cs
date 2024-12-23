using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectiles : EnemyDamage
{

    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifeTime;
    private Animator animator;
    private BoxCollider2D boxCollider;

    private bool hit;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    public void ActivateProjectile()
    {
        hit = false;
        lifeTime = 0;
        gameObject.SetActive(true);
        boxCollider.enabled = true;
    }

  

    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        lifeTime += Time.deltaTime;
        if(lifeTime > resetTime) 
            gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        base.OnTriggerEnter2D(collision);
        boxCollider.enabled = false;

        if (animator != null)
        {
            //animator.SetTrigger("explode");
            DeActivate();
        }
        else
        {
            gameObject.SetActive(false);
        }

    }
    private void DeActivate()
    {
        gameObject.SetActive(false);
    }
}
