using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHaver : MonoBehaviour
{

    public float maxHealth;
    public float currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void die()
    {
        Destroy(this.gameObject);
    }

    public float takeDamage(float damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            die();
        }

        return damage;
    }

}
