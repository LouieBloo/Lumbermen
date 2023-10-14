using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static AllUnitPrefabs;

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
    private float baseAttackRadius = 1f;
    [SerializeField]
    private float baseMaxHealthPerStrength = 1f;
    [SerializeField]
    private float baseHealthRegenPerStrength = 1f;

    public bool isEnemy = true;

    [SerializeField]
    private BasicUnitMovement.MovementType baseMovementType;

    Dictionary<StatTypes, UnitStat> stats = new Dictionary<StatTypes, UnitStat>();

    private List<Modification> modifications = new List<Modification>();

    public EquipmentHolder equipmentHolder;

    public Transform weaponLocation;

    public class UnitStat
    {
        private float amount;
        private float modifiedAmount;
        public StatTypes type;

        private List<Action<float>> callbacks = new List<Action<float>>();

        public UnitStat(StatTypes type, float amount) {
            this.type = type;
            this.amount = amount;
        }

        public float total
        {
            get { return amount + modifiedAmount; }
        }

        public float subscribe(Action<float> callback)
        {
            callbacks.Add(callback);
            flushCallbacks();
            return total;
        }

        public void modifyAmount(float amountToModify)
        {
            amount += amountToModify;
            flushCallbacks();
        }

        public void modifyModifyAmount(float amountToModify)
        {
            modifiedAmount += amountToModify;
            flushCallbacks();
        }

        private void flushCallbacks()
        {
            foreach (var callback in callbacks)
            {
                callback(total);
            }
        }
    }

    public enum StatTypes { 
        Strength, Agility, Intelligence, 
        MovementSpeed, 
        Health, HealthRegen, MaxHealth, 
        AttackSpeed, AttackDamage, AttackRange, IsMelee, AttackRadius,
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
        stats[StatTypes.AttackRadius] = new UnitStat(StatTypes.AttackRadius, baseAttackRadius);
    }

    public void modifyStat(StatTypes type, float amount)
    {
        if (stats.TryGetValue(type, out UnitStat targetStat))
        {
            targetStat.modifyAmount(amount);
        }
    }

    public float subscribeToStat(StatTypes type, Action<float> callback)
    {
        if (stats.TryGetValue(type, out UnitStat targetStat))
        {
            return targetStat.subscribe(callback);
        }

        return 0;
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
    public float attackRadius { get { return stats[StatTypes.AttackRadius].total; } }

    public BasicUnitMovement.MovementType movementType { get { return baseMovementType; } }

    public void addModifier(IModifier modifierToAdd)
    {
        foreach(Modification mod in modifierToAdd.getModifications())
        {
            modifications.Add(mod);
            recalculateModifier(mod.statType, mod.amount);
        }
    }

    public void deleteModifier(IModifier modifierToDelete)
    {
        
        // notice the - on amount
        foreach (Modification mod in modifierToDelete.getModifications())
        {
            modifications.Remove(mod);
            recalculateModifier(mod.statType, -mod.amount);
        }
    }

    public void recalculateModifier(StatTypes statType, float amount)
    {
        stats[statType].modifyModifyAmount(amount);
    }

    public void wow()
    {
        foreach (Modification mod in modifications)
        {
            Debug.Log(mod.statType + " " + mod.amount);
        }
    }

}
