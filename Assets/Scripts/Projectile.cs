using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;

    private BoxCollider2D BoxCollider;
    private Animator anim;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        BoxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        BoxCollider.enabled = false;
        if(collision.tag == "Enemy")
        {
            Debug.Log("Enemy Is Hit on the target");
            collision.GetComponent<Health>().TakeDamage(1);
        }
        
        //anim.SetTrigger("explode");
        //anim.SetTrigger("explode");
        Deactivate();
    }
    public void SetDirection(float _direction)
    {
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        BoxCollider.enabled =  true;


        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX) != _direction)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);

    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
