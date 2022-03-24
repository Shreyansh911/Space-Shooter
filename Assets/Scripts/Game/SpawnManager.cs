using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyContainer;
    [SerializeField] private GameObject[] PowerUps;
    private bool _stopSpawning = true;

    // Start is called before the first frame update
    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyMan());
        StartCoroutine(SpawnPowerUpMan());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemyMan()
    {
        yield return new WaitForSeconds(3);

        while (_stopSpawning)
        {
            Vector3 SpawnPosition = new Vector3(Random.Range(-10f, 10f), 5.5f, 0);


            GameObject newEnemy = Instantiate(_enemyPrefab, SpawnPosition, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(3);
        }
    }

    IEnumerator SpawnPowerUpMan()
    {
        yield return new WaitForSeconds(4);

       while (_stopSpawning)
        {
            Vector3 SpawnPosition = new Vector3(Random.Range(-8, 8), 5.5f, 0);
            

            Instantiate(PowerUps[Random.Range(0,3)], SpawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(6, 10));
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = false;
    }
}
