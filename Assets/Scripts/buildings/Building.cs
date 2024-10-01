using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Entity
{
    public bool destroyed = false;
    [SerializeField] int currentlife;
    [SerializeField] BuildingDefinition definition;
    [SerializeField] float respawnTime;
    private void Awake()
    {
        currentlife = definition.MaxLife;
    }
    public override void TakeDamage(int damage)
    {
       int newDamage = definition.Armor(damage);
        currentlife -= newDamage;
        if (currentlife <= 0)
            Die();
    }
    
    void Die()
    {
        destroyed = true;
        gameObject.SetActive(false);
        RespawnManager.Instance.RespawnBuilding(this, respawnTime);
    }

    public void ResetHealth()
    {
        currentlife = definition.MaxLife;
        destroyed = false;
    }
}
