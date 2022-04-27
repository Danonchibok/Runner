using UnityEngine;

public class RoadTile : MonoBehaviour
{
    public ObstacleSpawner ObstacleSpawner { get; private set; }
    private GameObject _obstacles;
   
    public void InitTile(ObstacleSpawner obstacleSpawner)
    {
        ObstacleSpawner = obstacleSpawner;
        _obstacles = ObstacleSpawner.SpawnObstaclesOnTile(this);
    }

    public void OnStart()
    {
        Destroy(_obstacles);
    }
}
