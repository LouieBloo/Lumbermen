using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LumberjackAttack : MonoBehaviour
{
    public float baseAttackRange = 0.5f;
    public float baseAttackRadius = 0.5f;
    public float baseAttackSpeed = 1f;
    public float baseDamage = 10f;
    public float damagePerAgility = 5f;
    public float attackSpeedPerAgility = 0.05f;

    private float attackRange;
    private float attackRadius;
    private float attackSpeed;
    private float damage;

    public float autoAttackTurnOnRadius = 10f;

    private float attackTimer = 99f;

    public Animator animator;
    public Rigidbody2D rigidbody;

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

        playerLayer = LayerMask.NameToLayer("Player");
        treeLayer = LayerMask.GetMask("Trees");

        lastMovementDirection = Vector2.zero;

        attackRange = baseAttackRange;
        attackRadius = baseAttackRadius;
        attackSpeed = baseAttackSpeed;
        damage = baseDamage;
    }

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;

        if (canAttack())    
        {
            attack();
        }
        Vector2 moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        if (moveDirection.sqrMagnitude > 0)
        {
            lastMovementDirection = moveDirection;
        }
    }

    float getDamage()
    {
        return damage + (damagePerAgility * unit.agility);
    }

    float getAttackSpeed()
    {
        return attackSpeed - (attackSpeedPerAgility * unit.agility);
    }

    void attack()
    {
        animator.SetTrigger("Attack");
        attackTimer = 0;

        // Get the direction to the collider we collided with
        //Vector2 direction = rigidbody.velocity.normalized;
        
        // Store values for gizmo drawing
        lastCastDirection = lastMovementDirection;
        lastCastRadius = attackRadius; // Set this to whatever radius you want
        lastCastDistance = attackRange; // Set this to whatever distance you want
        lastCastOrigin = transform.position;

        // Perform a CircleCast in that direction

        int layerMask = ~(1 << playerLayer); // This excludes the player layer
        RaycastHit2D[] hits = Physics2D.CircleCastAll(lastCastOrigin, lastCastRadius, lastCastDirection, lastCastDistance, layerMask);

        // If the CircleCast hit something
        if (hits != null && hits.Length > 0)
        {
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null)
                {
                    HealthHaver healthHaver = hit.collider.gameObject.GetComponent<HealthHaver>();
                    if (healthHaver != null)
                    {
                        healthHaver.takeDamage(getDamage());
                    }
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
