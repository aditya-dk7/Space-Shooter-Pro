﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    private float _powerUpSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _powerUpSpeed);

        if(transform.position.y < -3.638f)
        {
            
            Destroy(this.gameObject);
        }

       

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.TripleShotActive();
            }
            Destroy(this.gameObject);
        }
    }
}
