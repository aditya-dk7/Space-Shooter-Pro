using System.Collections;
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

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0,0,0); 
    }

    

    // Update is called once per frame
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
        Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.5f, 0), Quaternion.identity);
     }

    public void Damage()
    {
        _lives--;

        if(_lives == 0)
        {
            Destroy(this.gameObject);
        }
    }

}
