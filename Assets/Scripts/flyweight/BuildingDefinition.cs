using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "building", menuName = "scriptables/entities/building", order = 1)]
public class BuildingDefinition : ScriptableObject
{
    [SerializeField] int maxlife;
    [SerializeField] int armormultiplier;

    public int ArmorMultiplier => armormultiplier;
    public int MaxLife => maxlife;

    public int Armor( int basedamage)
    {
        return basedamage - armormultiplier;
    }

    
}
