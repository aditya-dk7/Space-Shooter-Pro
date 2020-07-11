using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _tripleShotPowerUpPrefab;
    private bool _stopSpawning = false;

    
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    
    void Update()
    {
        

    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(UnityEngine.Random.Range(-9f, 9f), 9.0f, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab,posToSpawn,Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }
    IEnumerator SpawnPowerupRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 postToSpawn = new Vector3(UnityEngine.Random.Range(-9f,9f),9f,0);
            Instantiate(_tripleShotPowerUpPrefab,postToSpawn,Quaternion.identity);
            yield return new WaitForSeconds(UnityEngine.Random.Range(8,15));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

}
