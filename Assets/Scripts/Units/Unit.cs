using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    private float baseStrength = 0f;
    [SerializeField]
    private float baseAgility = 0f;
    [SerializeField]
    private float baseIntelligence = 0f;
    [SerializeField]
    private float baseMovementSpeed = 1f;
    [SerializeField]
    private float baseMaximumHealth = 100f;
    [SerializeField]
    private float baseHealthRegen = 1f;
    [SerializeField]
    private float baseAttackSpeed = 1f;
    [SerializeField]
    private float baseAttackRange = 1f;
    [SerializeField]
    private float baseAttackDamage = 1f;
    [SerializeField]
    private float baseMaxHealthPerStrength = 1f;
    [SerializeField]
    private float baseHealthRegenPerStrength = 1f;

    [SerializeField]
    private bool isMeleeAttacker = true;

    Dictionary<StatTypes, UnitStat> stats = new Dictionary<StatTypes, UnitStat>();

    private List<Modifier> modifiers = new List<Modifier>();

    public class UnitStat
    {
        public float amount;
        public float modifiedAmount;
        public StatTypes type;

        public UnitStat(StatTypes type, float amount) {
            this.type = type;
            this.amount = amount;
        }

        public float total
        {
            get { return amount + modifiedAmount; }
        }
    }

    public enum StatTypes { 
        Strength, Agility, Intelligence, 
        MovementSpeed, 
        Health, HealthRegen, MaxHealth, 
        AttackSpeed, AttackDamage, AttackRange, IsMelee,
        MaxHealthPerStrength, HealthRegenPerStrength,
        Gold
    }

    private void Awake()
    {
        stats[StatTypes.Strength] = new UnitStat(StatTypes.Strength, baseStrength);
        stats[StatTypes.Agility] = new UnitStat(StatTypes.Agility, baseAgility);
        stats[StatTypes.Intelligence] = new UnitStat(StatTypes.Intelligence, baseIntelligence);
        stats[StatTypes.Health] = new UnitStat(StatTypes.Health, baseMaximumHealth);
        stats[StatTypes.MaxHealth] = new UnitStat(StatTypes.MaxHealth, baseMaximumHealth);
        stats[StatTypes.HealthRegen] = new UnitStat(StatTypes.HealthRegen, baseHealthRegen);
        stats[StatTypes.AttackSpeed] = new UnitStat(StatTypes.AttackSpeed, baseAttackSpeed);
        stats[StatTypes.MaxHealthPerStrength] = new UnitStat(StatTypes.MaxHealthPerStrength, baseMaxHealthPerStrength);
        stats[StatTypes.HealthRegenPerStrength] = new UnitStat(StatTypes.HealthRegenPerStrength, baseHealthRegenPerStrength);
        stats[StatTypes.MovementSpeed] = new UnitStat(StatTypes.MovementSpeed, baseMovementSpeed);
        stats[StatTypes.AttackRange] = new UnitStat(StatTypes.AttackRange, baseAttackRange);
        stats[StatTypes.AttackDamage] = new UnitStat(StatTypes.AttackDamage, baseAttackDamage);
        stats[StatTypes.IsMelee] = new UnitStat(StatTypes.IsMelee, isMeleeAttacker ? 1 : 0);
    }

    public void modifyStat(StatTypes type, float amount)
    {
        if (stats.TryGetValue(type, out UnitStat targetStat))
        {
            targetStat.amount += amount;
        }
    }

    public float strength { get { return stats[StatTypes.Strength].total; }  }
    public float agility { get { return stats[StatTypes.Agility].total; } }
    public float intelligence { get { return stats[StatTypes.Intelligence].total; } }
    public float health { get { return stats[StatTypes.Health].total; } }
    public float maxHealth { 
        get { 
            return stats[StatTypes.MaxHealth].total + (strength * maxHealthPerStrength);
        }
    }
    public float healthRegen
    {
        get{
            return stats[StatTypes.HealthRegen].total + (strength * healthRegenPerStrength);
        }
    }

    public float maxHealthPerStrength { get { return stats[StatTypes.MaxHealthPerStrength].total; } }
    public float healthRegenPerStrength { get { return stats[StatTypes.HealthRegenPerStrength].total; } }
    public float movementSpeed { get { return stats[StatTypes.MovementSpeed].total; } }

    public float isMelee { get { return stats[StatTypes.IsMelee].total; } }


    public float attackSpeed { get { return stats[StatTypes.AttackSpeed].total; } }
    public float attackRange { get { return stats[StatTypes.AttackRange].total; } }
    public float attackDamage { get { return stats[StatTypes.AttackDamage].total; } }

    public void recalculateModifiers()
    {

    }

}
