using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : Weapon
{
    protected Vector2 lastMovementDirection;

    public float autoAttackTurnOnRadius = 10f;

    private int playerLayer;
    private int treeLayer;

    //debug stuff
    private Vector2 lastCastDirection;
    private float lastCastRadius;
    private float lastCastDistance;
    private Vector2 lastCastOrigin;

    protected override void Start()
    {
        base.Start();
        lastMovementDirection = Vector2.zero;

        playerLayer = LayerMask.NameToLayer("Player");
        treeLayer = LayerMask.GetMask("Trees");
    }

    protected override void Update()
    {
        base.Update();

        Vector2 moveDirection = new Vector2(GameSettings.Instance.getXDirection(), GameSettings.Instance.getYDirection()).normalized;

        if (moveDirection.sqrMagnitude > 0)
        {
            lastMovementDirection = moveDirection;
        }
    }

    protected override bool canAttack()
    {
        return !attacking && attackTimer >= getAttackSpeed() && isAroundTrees();
    }

    protected virtual bool isAroundTrees()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, autoAttackTurnOnRadius, treeLayer);
        bool aroundTrees = hits != null && hits.Length > 0;
        return aroundTrees;
    }

    public override void attackAnimationFinished()
    {
        attackTimer = 0;
        attacking = false;

        // Store values for gizmo drawing
        lastCastDirection = lastMovementDirection;
        lastCastRadius = getRadius(); // Set this to whatever radius you want
        lastCastDistance = getRange(); // Set this to whatever distance you want
        //lastCastOrigin = transform.position;
        lastCastOrigin = (Vector2)transform.position + (lastCastDirection.normalized * lastCastDistance);

        // Perform a CircleCast in that direction

        int layerMask = ~(1 << playerLayer); // This excludes the player layer
        //RaycastHit2D[] hits = Physics2D.CircleCastAll(lastCastOrigin, lastCastRadius, lastCastDirection, lastCastDistance, layerMask);
        Collider2D[] hits = Physics2D.OverlapCircleAll(lastCastOrigin, lastCastRadius, layerMask);

        // If the CircleCast hit something
        if (hits != null && hits.Length > 0)
        {
            foreach (Collider2D hit in hits)
            {
                HealthHaver healthHaver = hit.gameObject.GetComponent<HealthHaver>();
                if (healthHaver != null)
                {
                    healthHaver.takeDamage(getDamage(), gameObject);
                }
            }
        }
    }

    void OnDrawGizmos()
    {
        // Draw the CircleCast as a line and a circle at the end
        Gizmos.color = Color.red;
        Gizmos.DrawLine(lastCastOrigin, lastCastOrigin + lastCastDirection * lastCastDistance);
        Gizmos.DrawWireSphere(lastCastOrigin + lastCastDirection * lastCastDistance, lastCastRadius);

       // Gizmos.DrawWireSphere   (transform.position, autoAttackTurnOnRadius);
    }
}
