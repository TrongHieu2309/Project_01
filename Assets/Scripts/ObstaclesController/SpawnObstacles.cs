using System.Collections;
using UnityEngine;

public class SpawnObstacles : MonoBehaviour
{
    [SerializeField] private GameObject[] _obstacles;
    [SerializeField] private Transform _birdPosition;
    public int indexBird;

    public float _spawnCooldown;
    public float _spawnTimer;

    void Start()
    {
        _spawnCooldown = Random.Range(3, 5);
        _spawnTimer = 0f;
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
        }
    }

    private void SpawnObstacle()
    {
        int index = Random.Range(0, _obstacles.Length - 1);
        Instantiate(_obstacles[index], transform.position, Quaternion.identity);

        indexBird = Random.Range(1, 3);
        if (indexBird == 2)
        {
            _spawnTimer = 0;
            Instantiate(_obstacles[6], _birdPosition.position, Quaternion.identity);
        }
        else if (indexBird == 1)
        {
            _spawnTimer = 0;
        }
    }

    IEnumerator SpawnBird()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(_obstacles[6], _birdPosition.position, Quaternion.identity);
    }

    public void SpawnCrate()
    {
        Instantiate(_obstacles[0], transform.position, Quaternion.identity);
        _spawnTimer = 0;
    }
}
