using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _enemySpeed = 2f;
    private GameObject _laserPrefab;
    // Start is called before the first frame update
   
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _enemySpeed);
        if(transform.position.y < -5f)
        {
            transform.position = new Vector3(Random.Range(-9f, 9f),9.0f , 0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
       
        if(other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player != null)
            {
                player.Damage();
                Destroy(this.gameObject);
            }
            
        }


        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);

        }
    }
}
