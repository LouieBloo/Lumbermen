using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamageWhenSurrounded : MonoBehaviour
{
    public bool enabled = true;
    public float damage = 10f;
    public float damageTickRate = 1f;
    public float castRadius = 1f;
    public int numberOfTreesToTakeDamage = 8;
    private float damageTickRateTimer = 0f;

    private int treeLayer;

    private void Start()
    {
        treeLayer = LayerMask.GetMask("Trees");
    }

    // Update is called once per frame
    void Update()
    {
        if(enabled)
        {
            damageTickRateTimer += Time.deltaTime;

            if(damageTickRateTimer > damageTickRate )
            {
                // Perform a CircleCast in that direction

                Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, castRadius, treeLayer);

                // If the CircleCast hit something
                if (hits != null && hits.Length >= numberOfTreesToTakeDamage)
                {
                    GetComponent<HealthHaver>().takeDamage(damage,null);
                    damageTickRateTimer = 0f;
                }
            }
        }
    }

}
