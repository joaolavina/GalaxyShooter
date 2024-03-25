using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public bool canTripleShot = false;
    public bool gottaGoFast = false;
    public bool isShieldUp = false;
    private int remainingLives = 3;

    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shieldGameObject;
    [SerializeField]
    private GameObject[] _engines;
    [SerializeField]
    private float _fireRate = 0.25f;
    [SerializeField]
    private GameObject _explosion;

    private int lastRandom;
    private float _canFire = 0.0f;
    private UIManager _uiManager;
    private GameManager _gameManager; 
    private SpawnManager _spawnManager;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _audioSource = GetComponent<AudioSource>();

        if (_uiManager != null)
        {
            _uiManager.UptadeLives(remainingLives);
        }

        if (_spawnManager != null)
        {
            _spawnManager.StartSpawnRoutines();
        }
    }

    // Update is called once per frame
    void Update()
    {

        Movement();

        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            Shoot();
        }

        if (remainingLives < 1)
        {
            _uiManager.showTitleScreen();
            Destroy(this.gameObject);
        }

    }

    private void Shoot()
    {
        if (Time.time > _canFire)
        {
            _audioSource.Play(); 
            if (canTripleShot)
            {
                Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);

            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.88f, 0), Quaternion.identity);
                _canFire = Time.time + _fireRate;
            }
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (gottaGoFast)
        {
            transform.Translate(Vector3.right * _speed * 2f * Time.deltaTime * horizontalInput);
            transform.Translate(Vector3.up * _speed * 2f * Time.deltaTime * verticalInput);
        }
        else
        {
            transform.Translate(Vector3.right * _speed * Time.deltaTime * horizontalInput);
            transform.Translate(Vector3.up * _speed * Time.deltaTime * verticalInput);
        }

        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        if (transform.position.x < -9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
        else if (transform.position.x > 9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
    }

    public void Damage()
    {
        if (isShieldUp)
        {

            isShieldUp = false;
            _shieldGameObject.SetActive(false);
            return;
        }

        remainingLives--;
        _uiManager.UptadeLives(remainingLives);

        int randomEngine = Random.Range(0,2);
        
        if(remainingLives == 2)
        {
            _engines[randomEngine].SetActive(true); 
            lastRandom = randomEngine;
        }
        else if(remainingLives == 1)
        {
            if(lastRandom == 0)
            {
                _engines[1].SetActive(true);
            }
            else if(lastRandom == 1)
            {
                _engines[0].SetActive(true);
            }
        }
        else if (remainingLives < 1)
        {
            Instantiate(_explosion, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
            _gameManager.gameOver = true;
            _uiManager.showTitleScreen();
        }
    }

    public void EnableTripleShot()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        canTripleShot = false;
    }

    public void EnableSpeedBoost()
    {
        StartCoroutine(SpeedBoostPowerDownRoutine());
        gottaGoFast = true;
    }

    public IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        gottaGoFast = false;
    }

    public void EnalbeShield()
    {
        isShieldUp = true;
        _shieldGameObject.SetActive(true);
    }
}

