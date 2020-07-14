using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _enemySpeed = 2f;
    private GameObject _laserPrefab;

    private Player _player;

    private Animator _anim;
    private AudioSource _audioSource;
    
    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
        
        if(_player == null)
        {
            Debug.LogError("The player is null.");
        }
        _anim = GetComponent<Animator>();

        if (_anim == null)
        {
            Debug.LogError("The Animator is null.");
        }

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
                _anim.SetTrigger("OnEnemyDeath");
                _enemySpeed = 0;
                _audioSource.Play();
                Destroy(this.gameObject, 2.8f);
                
            }
            
        }


        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.UpdateScore(Random.Range(5,10));
            }
            _anim.SetTrigger("OnEnemyDeath");
            _audioSource.Play();
            _enemySpeed = 0;
            Destroy(this.gameObject, 2.8f);
            
            


        }
    }
}
