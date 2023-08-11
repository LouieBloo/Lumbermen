using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHaver : MonoBehaviour
{

    public float baseMaxHealth;
    public float baseRegenPerSecond = 0f;
    public bool usePlayerStats = false;
    public float maxHealthPerStrength;
    public float regenPerStrength;
    private float maxHealth;
    private float currentHealth;
    private float regenPerSecond;


    public float hurtAudioClipVolume = 0.8f;
    public AudioClip hurtAudioClip;

    public RectTransform healthBar;
    private float healthBarStartingWidth;

    private IEnumerator healthRegenCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = baseMaxHealth;
        currentHealth = maxHealth;
        regenPerSecond = baseRegenPerSecond;

        if(regenPerSecond > 0)
        {
            healthRegenCoroutine = regenHealth();
            StartCoroutine(healthRegenCoroutine);
        }

        if(healthBar != null) { 
            healthBarStartingWidth = healthBar.localScale.x;
        }
    }

    IEnumerator regenHealth()
    {
        while(currentHealth > 0)
        {
            if(Time.timeScale > 0)
            {
                if(currentHealth < getMaxHealth())
                {
                    currentHealth += Time.deltaTime * getRegen();
                    if (currentHealth > getMaxHealth())
                    {
                        currentHealth = getMaxHealth();
                    }
                    updateHealthBar();
                }
            }

            yield return null;
        }
    }

    float getMaxHealth()
    {
        return maxHealth + (usePlayerStats ? (Player.Instance.strength * maxHealthPerStrength) : 0);
    }

    float getRegen()
    {
        return regenPerSecond + (usePlayerStats ? (Player.Instance.strength * regenPerStrength) : 0);
    }

    void die()
    {
        if(healthRegenCoroutine != null)
        {
            StopCoroutine(healthRegenCoroutine);
        }

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
        updateHealthBar();

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

    void updateHealthBar()
    {
        if(healthBar != null)
        {
            float percentage = currentHealth / getMaxHealth();
            healthBar.localScale = new Vector3(healthBarStartingWidth * percentage,healthBar.localScale.y, healthBar.localScale.z);
        }
    }
}
