using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModifier
{
    public Modification[] getModifications();
}

[System.Serializable]
public class Modification
{
    public Unit.StatTypes statType;
    public float amount;
}
