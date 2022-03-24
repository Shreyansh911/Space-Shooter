using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float _speed = 4;
    [SerializeField] private AudioClip _ExplosionSound;
    [SerializeField] private GameObject _enemyLaser;

    private AudioSource _audioSource;
    private Player _player;
    private Animator _enemyExplotion;
    private BoxCollider2D _boxCollider2d;
    private float _canFire = -1;
    private float _fireRate;
    private bool _isEnemyAlive = true;

    
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _enemyExplotion = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _boxCollider2d = GetComponent<BoxCollider2D>();



        if(_audioSource == null)
        {
            Debug.LogError("Audio Source In Enemy is null");
        }

        else
        {
            _audioSource.clip = _ExplosionSound;
        }
    }

    // Update is called once per frame
    void Update()
    {
        EnemyMovement();

        if (Time.time > _canFire)
        {
           
            if(_isEnemyAlive == true)
            {
                _fireRate = Random.Range(3, 5);
                _canFire = Time.time + _fireRate;
                Instantiate(_enemyLaser, transform.position, Quaternion.identity);
            }

        }
    }

    void EnemyMovement()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if (transform.position.y < -5.5f)
        {
            transform.position = new Vector3(Random.Range(-10f, 10f), 5.5f, 0);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
           
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            _enemyExplotion.SetTrigger("OnEnemyDeath");

            _speed = 0;

            Destroy(gameObject, 2.5f);

            _audioSource.Play();

            _isEnemyAlive = false;
        }

        if (other.tag == "Laser")
        {
            _boxCollider2d.enabled = false;

            Destroy(other.gameObject);



            if (_player != null)
            {
                _player.Score(10);
            }



            _enemyExplotion.SetTrigger("OnEnemyDeath");


            _speed = 0;

            Destroy(gameObject, 2.5f);



            _audioSource.Play();

            _isEnemyAlive = false;

        }
    }

   
}
