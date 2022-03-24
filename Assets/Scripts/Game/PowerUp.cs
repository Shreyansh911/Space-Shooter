using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private float _speed = 3;
    [SerializeField] private int _PowerUpNumber;
    [SerializeField] private AudioClip _audioClip;

    

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if (transform.position.y < -6)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag== "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            AudioSource.PlayClipAtPoint(_audioClip, transform.position); 

            if (player != null)
            {
                switch (_PowerUpNumber)
                {
                    case 0: player.TripleShotActive();
                        break;
                    case 1: player.SpeedBoost();
                            break;
                    case 2: player.Shield();
                        break;

                }

            }

            Destroy(gameObject);
        }
        
    }
}
