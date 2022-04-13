using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGeneretor : MonoBehaviour
{
    [SerializeField] private List<RoadTile> _roadTilesPrefabs;
    [SerializeField] private float _maxRoadTilesCount;
    [SerializeField] private float _endRoadOffset;

    private List<GameObject> _spawnedTiles;
    private Road _road;
    private ObstacleSpawner _obstacleSpawner;

    void Start()
    {
        _spawnedTiles = new List<GameObject>();
        _road = FindObjectOfType<Road>();
        _obstacleSpawner = FindObjectOfType<ObstacleSpawner>();
        StartLevel();
    }

    void Update()
    {
        if (_spawnedTiles.Count == 0) return;

        UpdateRoad();
    }

    private void SpawnRoadTile(bool isStartingTiles)
    {
        RoadTile tileToSpawn = _roadTilesPrefabs[Random.Range(0, _roadTilesPrefabs.Count)];
        Vector3 posToSpawn = Vector3.zero;

        if (_spawnedTiles.Count > 0)
        {
            posToSpawn = _spawnedTiles[_spawnedTiles.Count - 1].transform.position + new Vector3(0, 0, _endRoadOffset);
        }

        GameObject roadTile = Instantiate(tileToSpawn.gameObject, posToSpawn, tileToSpawn.gameObject.transform.rotation);

        if(!isStartingTiles) roadTile.GetComponent<RoadTile>().InitTile(_obstacleSpawner);
        
        _spawnedTiles.Add(roadTile);
        roadTile.transform.SetParent(_road.transform);
    }

    private void StartLevel()
    {
        for (int i = 0; i < _maxRoadTilesCount; i++)
        {
            SpawnRoadTile(true);
        }
    }

    private void UpdateRoad()
    {
        if (_spawnedTiles[0].gameObject.transform.position.z < _endRoadOffset)
        {
            Destroy(_spawnedTiles[0].gameObject);
            _spawnedTiles.RemoveAt(0);
            SpawnRoadTile(false);
        }
    }
}
