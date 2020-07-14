using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _enemySpeed = 2f;
    private GameObject _laserPrefab;

    private Player _player;
    
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        
    }

    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _enemySpeed);
        if(transform.position.y < -5f)
        {
            transform.position = new Vector3(Random.Range(-9f, 9f),9.0f , 0);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
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
            if (_player != null)
            {
                _player.UpdateScore(Random.Range(5,10));
            }
            
            Destroy(this.gameObject);


        }
    }
}
