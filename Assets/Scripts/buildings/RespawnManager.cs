using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RespawnBuilding(Building building, float respawnTime)
    {
        StartCoroutine(RespawnCoroutine(building, respawnTime));
    }

    private IEnumerator RespawnCoroutine(Building building, float respawnTime)
    {
        yield return new WaitForSeconds(respawnTime);
        building.gameObject.SetActive(true);
        building.ResetHealth();
    }
}
