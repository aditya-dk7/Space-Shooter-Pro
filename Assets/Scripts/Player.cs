using System.Collections;
using System.Data;
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
    private bool _isSpeedPowerupActive = false;
    private bool _isShieldActive = false;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private int _score = 0;
    private UIManager _uiManager;
    [SerializeField]
    private GameObject _leftEngine, _rightEngine;
    [SerializeField]
    private AudioClip _laserSoundClip;
    
    private AudioSource _audioSource;



    void Start()
    {
        transform.position = new Vector3(0,0,0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _audioSource = GetComponent<AudioSource>();
        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is null.");
        }
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is null.");
        }
        if(_audioSource == null)
        {
            Debug.LogError("The Audio Source is null.");
        }
        else
        {
            _audioSource.clip = _laserSoundClip;
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
        if (_isSpeedPowerupActive)
        {
            
            transform.Translate(new Vector3(Input.GetAxis("Horizontal"),
                                     Input.GetAxis("Vertical"), 0) * _speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(new Vector3(Input.GetAxis("Horizontal"),
                         Input.GetAxis("Vertical"), 0) * _speed * Time.deltaTime);
        }

        
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
        _audioSource.Play();

    }

    public void Damage()
    {
        if (_isShieldActive == false)
        {
            _lives--;
            _uiManager.UpdateLives(_lives);

            if(_lives == 2)
            {
                _leftEngine.SetActive(true);
            }else if(_lives == 1)
            {
                _rightEngine.SetActive(true);
            }

            if (_lives == 0)
            {
                _spawnManager.OnPlayerDeath();
                Destroy(this.gameObject);
                
            }
        }
        else
        {
            _isShieldActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }
        
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
        StartCoroutine(ShieldPowerDownRoutine());
    }
    public void SpeedPowerupActive()
    {
        _isSpeedPowerupActive = true;
        _speed += 5.0f;
        StartCoroutine(SpeedPowerDownRoutine());
    }

    IEnumerator ShieldPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _shieldVisualizer.SetActive(false);
        _isShieldActive = false;
    }

    

    IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _speed -= 5.0f;
        _isSpeedPowerupActive = false;

    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }

    public void UpdateScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }
}
