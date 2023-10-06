using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LumberjackAttack : MonoBehaviour
{
    public float damagePerAgility;
    public float attackSpeedPerAgility;
    public float attackRadiusMultiplier;
    public float attackRange = 0.5f;
    public float autoAttackTurnOnRadius = 10f;

    private float attackTimer = 99f;

    public Animator animator;
    public Rigidbody2D rigidbody;
    public CreatureAnimatorHelper animationHelper;

    private int playerLayer;
    private int treeLayer;

    //debug stuff
    private Vector2 lastCastDirection;
    private float lastCastRadius;
    private float lastCastDistance;
    private Vector2 lastCastOrigin;

    private Vector2 lastMovementDirection;

    private Unit unit;

    // Start is called before the first frame update
    void Start()
    {
        unit = GetComponent<Unit>();
        unit.subscribeToStat(Unit.StatTypes.AttackSpeed, unitStatUpdated);
        unit.subscribeToStat(Unit.StatTypes.Agility, unitStatUpdated);

        playerLayer = LayerMask.NameToLayer("Player");
        treeLayer = LayerMask.GetMask("Trees");

        lastMovementDirection = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;

        if (canAttack())    
        {
            attack();
        }
        Vector2 moveDirection = new Vector2(GameSettings.Instance.getXDirection(), GameSettings.Instance.getYDirection()).normalized;

        if (moveDirection.sqrMagnitude > 0)
        {
            lastMovementDirection = moveDirection;
        }
    }

    void unitStatUpdated(float stat)
    {
        animationHelper.setAttackAnimationSpeed(2.0f - getAttackSpeed());
    }

    float getDamage()
    {
        return unit.attackDamage + (damagePerAgility * unit.agility);
    }

    float getAttackSpeed()
    {
        return unit.attackSpeed - (attackSpeedPerAgility * unit.agility);
    }

    void attack()
    {
        animator.SetTrigger("Attack");
        attackTimer = 0;

        // Get the direction to the collider we collided with
        //Vector2 direction = rigidbody.velocity.normalized;
        
        // Store values for gizmo drawing
        lastCastDirection = lastMovementDirection;
        lastCastRadius = unit.attackRadius * attackRadiusMultiplier; // Set this to whatever radius you want
        lastCastDistance = attackRange; // Set this to whatever distance you want
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
                    healthHaver.takeDamage(getDamage(),gameObject);
                }
            }
        }
    }

    bool canAttack()
    {
        return attackTimer >= getAttackSpeed() && isAroundTrees();
    }

    bool isAroundTrees()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, autoAttackTurnOnRadius, treeLayer);
        bool aroundTrees = hits != null && hits.Length > 0;
        /*foreach (Collider2D hit in hits)
        {
            if(LayerMask.LayerToName(hit.gameObject.layer) == "Trees")
            {
                aroundTrees = true;
                break;
            }
        }*/

        return aroundTrees;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    void OnDrawGizmos()
    {
        // Draw the CircleCast as a line and a circle at the end
        Gizmos.color = Color.red;
        Gizmos.DrawLine(lastCastOrigin, lastCastOrigin + lastCastDirection * lastCastDistance);
        Gizmos.DrawWireSphere(lastCastOrigin + lastCastDirection * lastCastDistance, lastCastRadius);

        //Gizmos.DrawWireSphere   (transform.position, autoAttackTurnOnRadius);
    }
}
