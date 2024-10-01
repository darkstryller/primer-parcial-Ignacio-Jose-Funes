using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public static class pool
{
    public static Dictionary<elements, Component> EnemyLookUp = new Dictionary<elements, Component>();
    public static Dictionary<elements, Queue<Component>> EnemyPool = new Dictionary<elements, Queue<Component>>(); 

    public static void SetupEnemyPool<T>(T prefab, int instances, elements elementKey) where T: Component
    {
        if(!EnemyPool.ContainsKey(elementKey))
            EnemyPool.Add(elementKey, new Queue<Component>());
        

        
        for (int i = 0; i < instances; i++)
        {
            T pooledInstance = Object.Instantiate(prefab);
            pooledInstance.gameObject.SetActive(false);
            EnemyPool[elementKey].Enqueue(pooledInstance);
        }

        if (!EnemyLookUp.ContainsKey(elementKey))
            EnemyLookUp.Add(elementKey, prefab);
    }

    public static void enqueueEnemy<T>(T prefab, elements key) where T: Component
    {
        if (prefab.gameObject.activeSelf) return;

        prefab.transform.position = Vector3.zero;
        EnemyPool[key].Enqueue(prefab);
        prefab.gameObject.SetActive(false);
    }

    public static T dequeueEnemy<T>(elements key) where T: Component
    {
        if(EnemyPool[key].TryDequeue(out var item))
        {
            item.gameObject.SetActive(true);
            return (T)item;
        }
        return (T)enqueueNewEnemy(EnemyLookUp[key], key);
    }

    public static T enqueueNewEnemy<T>(T prefab, elements key)where T: Component
    {
        T newInstance = Object.Instantiate(prefab);
        newInstance.gameObject.SetActive(true);
        newInstance.transform.position = Vector3.zero;
        EnemyPool[key].Enqueue(newInstance);
        return newInstance;
    }
}
