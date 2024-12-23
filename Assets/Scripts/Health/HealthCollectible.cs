using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{

    [SerializeField] private float healthValue;
    [SerializeField] private AudioClip PickUpSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            SoundManager.instance.PlaySound(PickUpSound);
            collision.GetComponent<Health>().AddHealth(1);
            Destroy(gameObject);
        }
    }
}
