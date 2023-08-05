using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHaver : MonoBehaviour
{

    public float maxHealth;
    public float currentHealth;
    public float hurtAudioClipVolume = 0.8f;
    public AudioClip hurtAudioClip;

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
        else
        {
            Destroy(this.gameObject);
        }
        
    }

    public float takeDamage(float damage)
    {
        currentHealth -= damage;
        playDamageSound();

        if (currentHealth <= 0)
        {
            die();
        }
        

        return damage;
    }

    void playDamageSound()
    {
        if (hurtAudioClip != null)
        {
            AudioSource.PlayClipAtPoint(hurtAudioClip, transform.position, hurtAudioClipVolume);
        }
    }

}
