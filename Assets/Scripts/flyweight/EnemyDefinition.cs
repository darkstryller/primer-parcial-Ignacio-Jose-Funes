using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "enemy", menuName = "scriptables/entities/enemy", order = 0)]
public class EnemyDefinition : ScriptableObject
{
    [SerializeField] int maxlife;
    [SerializeField] int currentlife;
    [SerializeField] int damage;
    [SerializeField] float speed;
    


    public int Damage => damage;
    public float Speed => speed;
    public int MaxLife => maxlife;
}
