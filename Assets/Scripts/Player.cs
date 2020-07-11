﻿using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 10f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.2f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
    private bool _isTripleShotActive = false;
    [SerializeField]
    private GameObject _tripleShotPrefab;

    
    void Start()
    {
        transform.position = new Vector3(0,0,0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        if(_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is null.");
        }
    }

    

   
    void Update()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            ShootLaser();
        }
    }

    void CalculateMovement()
    {
        //float horizontalInput = Input.GetAxis("Horizontal");
        //float verticalInput = Input.GetAxis("Vertical");
        // It is similar to write Vector3(1,0,0) * Time * 5 -> Makes it go 5m/sec
        //transform.Translate(Vector3.right * Time.deltaTime * speed * horizontalInput);
        //transform.Translate(Vector3.up * Time.deltaTime * speed * verticalInput);

        //The instructor made me realize that I was using two new vectors to ddo the same movement
        //Thefore, the above is commented out and I have a new method to approach it
        transform.Translate(new Vector3(Input.GetAxis("Horizontal"),
             Input.GetAxis("Vertical"), 0) * _speed * Time.deltaTime);
        if (transform.position.y >= 2)
        {
            transform.position = new Vector3(transform.position.x, 2, 0);
        }
        else if (transform.position.y <= -2f)
        {
            transform.position = new Vector3(transform.position.x, -2f, 0);
        }

        if (transform.position.x > 9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
    }

    void ShootLaser()
    {
        _canFire = Time.time + _fireRate;
        if (_isTripleShotActive)
        {
            Instantiate(_tripleShotPrefab, transform.position , Quaternion.identity);

        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);

        }

    }

    public void Damage()
    {
        _lives--;

        if(_lives == 0)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

}
