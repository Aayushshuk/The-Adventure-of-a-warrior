using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;

    [Header("Components")]
    [SerializeField]  private Behaviour[] components;

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;
    private void Awake()
    {
        currentHealth = startingHealth;
        anim= GetComponent<Animator>();
    }
    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);

        if(currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            SoundManager.instance.PlaySound(hurtSound);
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");

                //Player 
                //It deactivates all the enemy.

                foreach (Behaviour component in components)
                {
                    component.enabled = false;
                }
                dead = true;
                SoundManager.instance.PlaySound(deathSound);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
           
        }
    }

   
    public void AddHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0 , startingHealth);
    }
   

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
