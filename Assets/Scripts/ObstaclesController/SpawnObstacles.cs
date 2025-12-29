using System.Collections;
using System.Threading;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    [SerializeField] private GameObject[] _obstacles;
    [SerializeField] private GameObject[] _fruits;
    [SerializeField] private Transform _birdPosition;
    public int indexBird;
    public int indexFruit;

    public float _spawnCooldown;
    public float _spawnTimer;

    public float _spawnFruitTimer;
    public float _spawnFruitCooldown;

    private float _durationBird;
    private float _durationFruit;

    void Start()
    {
        _spawnCooldown = Random.Range(3, 5);
        _spawnTimer = 0f;
        _durationFruit = 0.5f;
        _spawnFruitCooldown = 8f;
        _spawnFruitTimer = 0f;
    }

    void Update()
    {
        if (GameManager.Instance.IsPlaying)
        {
            _spawnTimer += Time.deltaTime;

            if (_spawnTimer >= _spawnCooldown)
            {
                SpawnObstacle();
                _spawnCooldown = Random.Range(3, 5);
            }

            if (_spawnFruitTimer <= _spawnFruitCooldown)
            {
                _spawnFruitTimer += Time.deltaTime;
            }
        }
    }

    private void SpawnObstacle()
    {
        int index = Random.Range(0, _obstacles.Length - 1);
        Instantiate(_obstacles[index], transform.position, Quaternion.identity);

        if (index == 5)
        {
            _durationBird = 1f;
        }
        else if (index == 3 || index == 4)
        {
            _durationBird = 0.48f;
        }
        else
        {
            _durationBird = 0.8f;
        }

        indexBird = Random.Range(1, 3);
        if (indexBird == 2)
        {
            _spawnTimer = 0;
            StartCoroutine(nameof(SpawnBird));
        }
        else if (indexBird == 1)
        {
            _spawnTimer = 0;
        }

        if (_spawnFruitTimer > _spawnFruitCooldown)
        {
            indexFruit = Random.Range(1, 4);
        }

        if (indexFruit == 3)
        {
            StartCoroutine(nameof(SpawnFruits));
            _spawnFruitTimer = 0f;
        }
    }

    IEnumerator SpawnBird()
    {
        yield return new WaitForSeconds(_durationBird);
        Instantiate(_obstacles[6], _birdPosition.position, Quaternion.identity);
    }
    
    IEnumerator SpawnFruits()
    {
        yield return new WaitForSeconds(_durationFruit);
        Instantiate(_fruits[0], transform.position, Quaternion.identity);
        indexFruit = 1;
    }

    public void SpawnCrate()
    {
        Instantiate(_obstacles[0], transform.position, Quaternion.identity);
        _spawnTimer = 0;
    }
}
