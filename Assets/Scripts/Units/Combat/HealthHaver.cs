using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHaver : MonoBehaviour
{

    public float maxHealth;
    public float currentHealth;
    public AudioSource hurtAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void die()
    {
        IDier dier = this.gameObject.GetComponent<IDier>();
        if (dier != null) { 
            dier.die();
        }
        Destroy(this.gameObject);
    }

    public float takeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            die();
        }
        else if (hurtAudioSource != null)
        {
            hurtAudioSource.Play();
        }

        return damage;
    }

}
