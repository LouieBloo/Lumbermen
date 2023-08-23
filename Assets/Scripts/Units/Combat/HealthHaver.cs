using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HealthHaver : MonoBehaviour
{
    public float hurtAudioClipVolume = 0.8f;
    public AudioClip hurtAudioClip;

    public RectTransform healthBar;
    private float healthBarStartingWidth;

    private IEnumerator healthRegenCoroutine;

    private Unit unit;

    // Start is called before the first frame update
    void Start()
    {
        unit = GetComponent<Unit>();

        if (unit.healthRegen > 0)
        {
            healthRegenCoroutine = regenHealth();
            StartCoroutine(healthRegenCoroutine);
        }

        if(healthBar != null) { 
            healthBarStartingWidth = healthBar.localScale.x;
            updateHealthBar();
        }
    }

    IEnumerator regenHealth()
    {
        while(unit.health > 0)
        {
            if(Time.timeScale > 0)
            {
                if(unit.health < unit.maxHealth)
                {
                    unit.modifyStat(Unit.StatTypes.Health, Time.deltaTime * unit.healthRegen);
                    if (unit.health > unit.maxHealth)
                    {
                        unit.modifyStat(Unit.StatTypes.Health, unit.maxHealth);
                    }
                    updateHealthBar();
                }
            }

            yield return null;
        }
    }

    void die(GameObject source)
    {
        if(healthRegenCoroutine != null)
        {
            StopCoroutine(healthRegenCoroutine);
        }

        IDier dier = this.gameObject.GetComponent<IDier>();
        if (dier != null) {
            dier.die(source);
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }

    public float takeDamage(float damage, GameObject source)
    {
        unit.modifyStat(Unit.StatTypes.Health, -damage);
        playDamageSound();
        updateHealthBar();

        if (unit.health <= 0)
        {
            die(source);
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
            float percentage = unit.health / unit.maxHealth;
            
            if(percentage >= 1f)
            {
                healthBar.localScale = new Vector3(0, 0, 0);
            }
            else
            {
                healthBar.localScale = new Vector3(healthBarStartingWidth * percentage, 1, 1);
            }
        }
    }
}
