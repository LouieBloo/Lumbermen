using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AllUnitPrefabs;

public class PlayerRangedWeapon : PlayerWeapon
{
    public ProjectileName projectileName;
    public float projectileSpeed;
    
    void Start()
    {
        base.Start();
    }

    void Update()
    {
        base.Update();
    }

    protected override bool isAroundTrees()
    {
        return true;
    }

    public override void attackAnimationFinished()
    {
        attackTimer = 0;
        attacking = false;

        // Instantiate the projectile at the player's position
        GameObject projectile = Instantiate(AllUnitPrefabs.Instance.getProjectile(projectileName), transform.position, Quaternion.identity);

        // Calculate the angle in degrees the projectile should face
        float angle = Mathf.Atan2(lastMovementDirection.y, lastMovementDirection.x) * Mathf.Rad2Deg;

        // Set the rotation of the projectile
        projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        projectile.GetComponent<Projectile>().setup(gameObject, lastMovementDirection.normalized * projectileSpeed, getDamage(), playerLayerMask);

        // Optionally, add force to the projectile if it should move
        /*Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = lastMovementDirection.normalized * projectileSpeed; // 'projectileSpeed' is a variable you define
        }*/
    }
}
