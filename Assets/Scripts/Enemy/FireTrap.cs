using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float damage;


    [Header("FireTrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer sprite;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip fireTrapSound;

    private bool trigerred;
    private bool active;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (!trigerred)
            {
                StartCoroutine(ActivateFireTrap()); 
            }
            if (active)
            {
                other.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }
    private IEnumerator ActivateFireTrap()
    {
        trigerred = true;
        sprite.color = Color.red;

        yield return new WaitForSeconds(activationDelay);
        SoundManager.instance.PlaySound(fireTrapSound);
        sprite.color = Color.white;
        active = true;
        anim.SetBool("activated", true);

        yield return new WaitForSeconds(activeTime);
        active = false;
        trigerred = false;
        anim.SetBool("activated", false);
    }
}
