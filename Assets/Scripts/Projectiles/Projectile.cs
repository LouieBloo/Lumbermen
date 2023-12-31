using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime;
    float lifeTimer = 0;
    float damage;
    GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lifeTimer += Time.deltaTime;
        if(lifeTimer >= lifetime) { Destroy(gameObject); }
    }

    public void setup(GameObject parent, Vector2 velocity, float damage, LayerMask excludeLayer)
    {
        this.damage = damage;
        this.parent = parent;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.excludeLayers = excludeLayer;

        if (rb != null)
        {
            rb.velocity = velocity;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HealthHaver[] healthHaver = collision.gameObject.GetComponents<HealthHaver>();
        if (healthHaver.Length > 0)
        {
            healthHaver[0].takeDamage(damage, parent);
            Destroy(gameObject);
        }
        else
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.healthHaver.takeDamage(damage, parent);
                Destroy(gameObject);
            }
        }
    }
}
