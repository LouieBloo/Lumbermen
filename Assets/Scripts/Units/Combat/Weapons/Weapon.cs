using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public class Weapon : Item
{
    public float damage;
    public float damagePerAgility;
    public float damagePerStrength;
    public float damagePerIntelligence;

    public float attackSpeed;
    public float attackSpeedPerAgility;
    
    public float attackRange = 1f;
    public float attackRangeMultiplier;

    public float attackRadius;
    public float attackRadiusMultiplier;

    protected float attackTimer = 99f;

    protected bool attacking = false;

    private LayerMask playerLayerMask;
    private LayerMask enemyLayerMask;
    private LayerMask targetLayerMask;

    [SerializeField]
    private CreatureAnimatorHelper animationHelper;
    private CreatureAnimatorHelper unitAnimationHelper;

    private Unit unit;

    private GameObject target;

    void Awake()
    {
        playerLayerMask = 1 << LayerMask.NameToLayer("Player");
        enemyLayerMask = 1 << LayerMask.NameToLayer("Enemies");
    }

    // Start is called before the first frame update
    void Start()
    {
        animationHelper.subscribeAttack(attackAnimationFinished);
    }

    public void setup(Unit unitIn, CreatureAnimatorHelper animationHelper)
    {
        unit = unitIn;
        this.unitAnimationHelper = animationHelper;

        unit.subscribeToStat(Unit.StatTypes.AttackSpeed, attackSpeedAnimationUpdate);
        unit.subscribeToStat(Unit.StatTypes.Agility, attackSpeedAnimationUpdate);

        targetLayerMask = unit.isEnemy ? playerLayerMask : enemyLayerMask;
    }

    void Update()
    {
        attackTimer += Time.deltaTime;

        if (canAttack())
        {
            attack();
        }
    }

    void attack()
    {
        attacking = true;

        if(target != null)
        {
        }

        animationHelper.animator.SetTrigger("Attack");
        unitAnimationHelper.animator.SetTrigger("Attack");
    }

    public void attackAnimationFinished()
    {
        attackTimer = 0;
        attacking = false;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, getRadius(), targetLayerMask);
        // If the CircleCast hit something
        if (hits != null && hits.Length > 0)
        {
            foreach (Collider2D hit in hits)
            {
                if (hit != null)
                {
                    HealthHaver healthHaver = hit.gameObject.GetComponent<HealthHaver>();
                    if (healthHaver != null)
                    {
                        healthHaver.takeDamage(getDamage(), gameObject);
                    }
                }
            }
        }
    }


    bool canAttack()
    {
        return !attacking && attackTimer >= getAttackSpeed() && isAroundEnemy();
    }

    bool isAroundEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, getRange(), targetLayerMask);
        if(hits != null && hits.Length > 0)
        {
            target = hits[0].gameObject;
            return true;
        }
        else
        {
            target = null;
            return false;
        }
    }

    void attackSpeedAnimationUpdate(float stat)
    {
        animationHelper.setAttackAnimationSpeed(2.0f - getAttackSpeed());
        unitAnimationHelper.setAttackAnimationSpeed(2.0f - getAttackSpeed());
    }

    float getAttackSpeed()
    {
        return attackSpeed - unit.attackSpeed - (attackSpeedPerAgility * unit.agility);
    }

    float getDamage()
    {
        return damage + unit.attackDamage + (damagePerAgility * unit.agility) + (damagePerStrength * unit.strength) + (damagePerIntelligence * unit.intelligence);
        //return damage + (unit.agi damagePerAgility * unit.agility);
    }

    protected virtual float getRange()
    {
        return attackRange + unit.attackRange;
    }

    protected virtual float getRadius()
    {
        return attackRadius + unit.attackRadius;
    }
}
