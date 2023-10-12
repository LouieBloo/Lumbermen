using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttacker : MonoBehaviour
{
    private float attackTimer = 99f;

    private Animator animator;
    private CreatureAnimatorHelper animationHelper;

    private int playerLayer;
    private LayerMask playerLayerMask;
    private int treeLayer;

    private Unit unit;
    private Transform positionToCheckForEnemies;

    private bool attacking = false;

    void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        playerLayerMask = 1 << playerLayer;
        treeLayer = LayerMask.GetMask("Trees");
    }

    public void setup(SpawnedEnemy newEnemy)
    {
        this.unit = newEnemy.unit;
        this.animationHelper = newEnemy.animationHelper;
        this.animator = newEnemy.animator;

        positionToCheckForEnemies = newEnemy.unit.weaponLocation;

        animationHelper.subscribeAttack(attackAnimationFinished);
    }

    // Update is called once per frame
    void Update()
    {
        //if(unit == null) { Debug.Log("nulll"); return; }
        attackTimer += Time.deltaTime;

        if (canAttack())
        {
            attack();
        }
    }

    float getDamage()
    {
        return unit.attackDamage;
        //return damage + (unit.agi damagePerAgility * unit.agility);
    }

    float getAttackSpeed()
    {
        return unit.attackSpeed;
        //return attackSpeed - (attackSpeedPerAgility * unit.agility);
    }

    void attack()
    {
        attacking = true;
        animator.SetTrigger("Attack");
    }

    public void attackAnimationFinished()
    {
        attackTimer = 0;
        attacking = false;

        Collider2D[] hits = Physics2D.OverlapCircleAll(positionToCheckForEnemies.position, unit.attackRange, playerLayerMask);
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
        Collider2D[] hits = Physics2D.OverlapCircleAll(positionToCheckForEnemies.position, unit.attackRange, playerLayerMask);
        bool aroundEnemies = hits != null && hits.Length > 0;

        return aroundEnemies;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

}
