using System;
using System.Collections;
using UnityEngine;
using Enemies;

 public enum elements
{
    Normal,
    Fire,
    Thunder,
    Ice
}
public class Spawner : MonoBehaviour
{
    public Enemy characterPrefab;
    [SerializeField] private elements myElement;
    [SerializeField] private Transform spawnpoint;
    [SerializeField] private int spawnsPerPeriod = 10;
    [SerializeField] private float frequency = 30;
    [SerializeField] private float period = 0;
    [SerializeField] private int myInstances = 10;

    private void Awake()
    {
        characterPrefab.myElement = myElement;
        setupPool();
    }

    private void OnEnable()
    {
        if (frequency > 0) period = 1 / frequency;       
    }

    private IEnumerator Start()
    {
        while (gameObject.activeSelf)
        {
            for (int i = 0; i < spawnsPerPeriod; i++)
            {
                Enemy instance = pool.dequeueEnemy<Enemy>(myElement);
                instance.gameObject.SetActive(true);
                instance.transform.position = spawnpoint.position;
                instance.initialize();

            }

            yield return new WaitForSeconds(period);
        }
    }

    void setupPool()
    {
        pool.SetupEnemyPool(characterPrefab, myInstances, myElement);
    }
}
