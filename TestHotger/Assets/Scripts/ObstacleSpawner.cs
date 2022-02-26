using System.Collections;
using System.Collections.Generic;
using System.Linq;
using QuickPool;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstacleObj;
    [SerializeField] private float highY;
    [SerializeField] private float leastY;
    [SerializeField] private Transform posSpawn;
    
    private float _bonusSpeed;
    private PoolsManager _poolsManager;
    private List<Obstacle> _obstacles = new List<Obstacle>();

    private void Start()
    {
        _poolsManager = PoolsManager.Instance;
        var pool = _poolsManager.pools[0].despawned.Where(x => x != _poolsManager.pools[0].prefab);
        foreach (var obstacle in pool)
        {
            _obstacles.Add(obstacle.GetComponent<Obstacle>());
        }
    }

    public void AddBonusSpeed(float speed)
    {
        _bonusSpeed += speed;
    }

    public void StartSpawn(float intervalSpawn)
    {
        foreach (var obstacle in _obstacles)
        {
            obstacle.SetSpeed(_poolsManager.pools[0].prefab.GetComponent<Obstacle>().GetSpeed());
        }
        
        StartCoroutine(nameof(SpawnObstacle), intervalSpawn);
    }

    public void DestroyAllObstacles()
    {
        StopCoroutine(nameof(SpawnObstacle));

        foreach (var obstacle in _obstacles)
        {
            if (obstacle.gameObject.activeSelf)
            {
                obstacle.gameObject.Despawn();
            }
        }
        _bonusSpeed = 0f;
    }
    
    private IEnumerator SpawnObstacle(float intervalSpawn)
    {
        do
        {
            yield return new WaitForSeconds(intervalSpawn);
            var position = posSpawn.position;
            Vector3 randPos = new Vector3(position.x, Random.Range(leastY, highY), position.z);
            obstacleObj.Spawn(randPos, Quaternion.identity);

            foreach (var obstacle in _obstacles)
            {
                obstacle.AddSpeed(_bonusSpeed);
            }
            
        } while (true);
    }
}
