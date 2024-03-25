using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemyShipPrefab;

    [SerializeField]
    private GameObject[] _powerups;

    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartSpawnRoutines()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerupSpawnRoutine());
    }

    public void enableSpawnManager()
    {
        this.gameObject.SetActive(true);
    }

    private IEnumerator EnemySpawnRoutine()
    {
        while (!_gameManager.gameOver)
        {
            float randomX = Random.Range(-7.7f, 7.7f);
            Instantiate(_enemyShipPrefab, new Vector3(randomX, 6.8f, 0), Quaternion.identity);
            yield return new WaitForSeconds(1.5f);
        }
    }

    private IEnumerator PowerupSpawnRoutine()
    {
        while (!_gameManager.gameOver)
        {
            float randomX = Random.Range(-7.7f, 7.7f);
            int powerupRandom = Random.Range(0, 3);
            Instantiate(_powerups[powerupRandom], new Vector3(randomX, 6.8f, 0), Quaternion.identity);
            yield return new WaitForSeconds(3.0f);
        }
    }

}
