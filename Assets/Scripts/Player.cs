﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private GameObject _tripleLaser;

    [SerializeField]
    private GameObject _playerShield;

    private float _canfire = -1f;

    [SerializeField]
    private float _fireRate = 0.5f;

    [SerializeField]
    private int _life = 3;

    [SerializeField]
    private float _powerUpCooldown = 5.0f;

    [SerializeField]
    private float _boost = 3;

    [SerializeField]
    private bool _isTripleShotActive;

    [SerializeField]
    private bool _isSpeedActive;

    [SerializeField]
    private bool _isShieldActive;

    private SpawnManager _spawnManager;

    private UIManager _uiManager;

    private int _score;

    // Start is called before the first frame update
    void Start()
    {
        transform.Translate(0, 0, 0);

        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        _isTripleShotActive = false;

        _isSpeedActive = false;

        _isShieldActive = false;

        _score = 0;

    }

    // Update is called once per frame
    void Update()
    {
        Move();
        FireLaser();
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -9.4f, 9.4f), Mathf.Clamp(transform.position.y, -3.4f, 0.0f), 0);

        if (_isSpeedActive)
        {

            transform.rotation = Quaternion.Euler(verticalInput * (_speed + _boost), horizontalInput * -_speed, 0);

            transform.Translate(direction * (_speed + _boost) * Time.deltaTime);

            return;
        }

        transform.rotation = Quaternion.Euler(verticalInput * _speed, horizontalInput * -_speed, 0);

        transform.Translate(direction * _speed * Time.deltaTime);

    }

    private void FireLaser()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canfire)
        {

            _canfire = Time.time + _fireRate;

            if (_isTripleShotActive)
            {
                Instantiate
                    (_tripleLaser,
                    new Vector3(transform.position.x, transform.position.y, transform.position.z),
                    Quaternion.identity
                    );

                return;
            }

            Instantiate
                (_laserPrefab, 
                new Vector3(transform.position.x, (transform.position.y + 1.26f), transform.position.z), 
                Quaternion.identity);
            
        } 
    }

    public void OnHit()
    {

        if (_isShieldActive)
        {
            _isShieldActive = false;
            _playerShield.SetActive(false);
            return;
        }

        _life -= 1;

        _uiManager.UpdateLives(_life);

        if (_life < 1)
        {
            _spawnManager.PlayerDead();

            Destroy(this.gameObject);

            _uiManager.GameOver();
        }

    }

    public void OnPowerUp(PowerUp powerUp)
    {

        switch (powerUp.getID())
        {
            case 0:

                _isTripleShotActive = true;
                StartCoroutine(TripleShotCooldown());

                break;

            case 1:

                _isSpeedActive = true;
                StartCoroutine(SpeedPowerUpCooldown());

                break;

            case 2:

                _playerShield.SetActive(true);
                _isShieldActive = true;
                
                break;

        }

    }

    private IEnumerator TripleShotCooldown()
    {
        yield return new WaitForSeconds(_powerUpCooldown);
        _isTripleShotActive = false;
    }

    private IEnumerator SpeedPowerUpCooldown()
    {
        yield return new WaitForSeconds(_powerUpCooldown);
        _isSpeedActive = false;
    }

    public void EnemyKilled()
    {
        _score += 10;

        _uiManager.UpdateScore(_score);

    }

}
