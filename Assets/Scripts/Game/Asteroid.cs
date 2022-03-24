using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] float _Rotatespeed = 3;
    [SerializeField] GameObject _explosionPrefab;
    [SerializeField] AudioClip _explosionSound;

    private AudioSource _audioSource;
    private SpawnManager _spawnManager;
    private CircleCollider2D _circleCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        _audioSource = GetComponent<AudioSource>();

        if(_spawnManager == null)
        {
            Debug.LogError("_spawnManager is null");
        }

        if(_audioSource == null)
        {
            Debug.LogError("Audio Source is Null in Asteroid");
        }
        else
        {
            _audioSource.clip = _explosionSound;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * Time.deltaTime * _Rotatespeed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {

            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.25f);
            Destroy(other.gameObject);



            _audioSource.Play();
        }
    }
}
