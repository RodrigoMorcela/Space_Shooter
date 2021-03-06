﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject _enemyContainer;

    [SerializeField]
    private GameObject _tripleShotPowerUp;

    [SerializeField]
    private GameObject _shieldPowerUp;

    [SerializeField]
    private GameObject _speedPowerUp;

    [SerializeField]
    private float _enemySpawnTime = 3.0f;

    [SerializeField]
    private float _powerUpSpawnTime = 10.0f;

    private bool _isPlayerDead;

    // Start is called before the first frame update
    void Start()
    {
        _isPlayerDead = false;
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    public void StartSpawning()
    {
        StartCoroutine(EnemySpawningRoutine());

        StartCoroutine(PowerUpSpawningRoutine());
    }

    private IEnumerator EnemySpawningRoutine()
    {

        yield return new WaitForSeconds(_enemySpawnTime);

        while (!_isPlayerDead) {

            Vector3 randomPos = new Vector3(Random.Range(-8f, 8f), 9f, 0f);

            if (_enemyPrefab != null)
            {
                GameObject newEnemy = Instantiate(_enemyPrefab,
                    randomPos,
                    Quaternion.identity);

                newEnemy.transform.parent = _enemyContainer.transform;
            }

            yield return new WaitForSeconds(_enemySpawnTime);

        }
    }

    private IEnumerator PowerUpSpawningRoutine()
    {

        yield return new WaitForSeconds(_powerUpSpawnTime);

        while (!_isPlayerDead)
        {

            yield return new WaitForSeconds(_powerUpSpawnTime);

            Vector3 randomPos = new Vector3(Random.Range(-8f, 8f), 9f, 0f);

            int randomNumb = Random.Range(0, 3);

            switch (randomNumb)
            {
                case 0:

                    Instantiate(_tripleShotPowerUp,
                        randomPos,
                        Quaternion.identity
                        );

                    break;

                case 1:

                    Instantiate(_speedPowerUp,
                        randomPos,
                        Quaternion.identity
                        );

                    break;

                case 2:

                    Instantiate(_shieldPowerUp,
                        randomPos,
                        Quaternion.identity
                        );

                    break;
            }
            
        }
    }

    public void PlayerDead()
    {
        _isPlayerDead = true;
    }

    public bool isPlayerDead()
    {
        return _isPlayerDead;
    }
}
