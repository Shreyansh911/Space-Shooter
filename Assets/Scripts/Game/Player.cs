using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _speedBooster = 2;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private GameObject _shieldPrefab;
    [SerializeField] private GameObject _rightWing;
    [SerializeField] private GameObject _leftWing;
    [SerializeField] private float _fireRate = 0.15f;
    [SerializeField] private float _canFire = -1;
    [SerializeField] private int _life = 3;
    [SerializeField] private int _score;
    [SerializeField] private int _highScore;
    [SerializeField] private bool _isTripleShotPowerUpActive = false;
    [SerializeField] private bool _isSpeedPowerUpActive = false;
    [SerializeField] private bool _isShildPowerUpActive = false;
    [SerializeField] private AudioClip _ShootingSound;
    private AudioSource _audioSource;
    private SpawnManager _spawnManager;
    protected Joystick _joystick;
    private UImanager _UImanager;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, -3, 0);
        _audioSource = GetComponent<AudioSource>();
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        _UImanager = GameObject.Find("Canvas").GetComponent<UImanager>();
        _joystick = FindObjectOfType<Joystick>();
        _animator = GetComponent<Animator>();

        if (_audioSource == null)
        {
            Debug.LogError("Audio Source in Player is Empty");
        }
        else
        {
            _audioSource.clip = _ShootingSound;
        }

       _rightWing.gameObject.SetActive(false);
       _leftWing.gameObject.SetActive(false);



    }

    // Update is called once per frame
    void Update()
    {
       // var rigidbody = GetComponent<Rigidbody2D>();

       // rigidbody.velocity = new Vector3(_joystick.Horizontal * _speed, _joystick.Vertical * _speed, 0);



        PlayerMovement();

        if(Input.GetKeyDown(KeyCode.Mouse0) && Time.time > _canFire)
        {
            FireLaser();
        }
        

        //var _rigidbody = GetComponent<Rigidbody>();


    }

    public void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");



        Vector3 directions = new Vector3(horizontalInput, verticalInput, 0);

        transform.Translate(directions * _speed * Time.deltaTime);

        if (transform.position.y > 5)
        {
            transform.position = new Vector3(transform.position.x, 5, 0);
        }

        else if (transform.position.y < -3.5f)
        {
            transform.position = new Vector3(transform.position.x, -3.5f, 0);
        }

        if (transform.position.x > 11.5f)
        {
            transform.position = new Vector3(-11.5f, transform.position.y, 0);
        }

        else if (transform.position.x < -11.5)
        {
            transform.position = new Vector3(11.5f, transform.position.y, 0);
        }

    }

    public void FireLaser()
    {
        _canFire = Time.time + _fireRate;

        if (_isTripleShotPowerUpActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }

        if (_isTripleShotPowerUpActive == false)
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.03f, 0), Quaternion.identity);
        }

        _audioSource.Play();

    }

   

    public void Damage()
    {
        if (_isShildPowerUpActive == true)
        {
            _isShildPowerUpActive = false;
            _shieldPrefab.SetActive(false);
            return;
        }

        _life--;

        if(_life == 2)
        {
            _rightWing.gameObject.SetActive(true);
        }

        else if(_life == 1)
        {
            _leftWing.gameObject.SetActive(true);
        }

        _UImanager.UpdateLifes(_life);

        if (_life < 1 && _isShildPowerUpActive != true)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(gameObject);
        }
    }

   



    public void TripleShotActive()
    {
        _isTripleShotPowerUpActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        
        yield return new WaitForSeconds(5);
        _isTripleShotPowerUpActive = false;
    }

    public void SpeedBoost()
    {
        _isSpeedPowerUpActive = true;
        
            _speed *= _speedBooster;
            StartCoroutine(SpeedBoostCoolDown());
        
    }

    IEnumerator SpeedBoostCoolDown()
    {
        yield return new WaitForSeconds(5);
        _isTripleShotPowerUpActive = false;
        _speed /= _speedBooster;
    }

    public void Shield()
    {
        _isShildPowerUpActive = true;
        _shieldPrefab.SetActive(true);
    }

    public void Score(int score)
    {
        _score += score;
        _UImanager.UpdateScore(_score);
    }
}
