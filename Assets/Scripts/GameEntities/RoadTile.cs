using UnityEngine;

public class RoadTile : MonoBehaviour
{
    public ObstacleSpawner ObstacleSpawner { get; private set; }
   
    public void InitTile(ObstacleSpawner obstacleSpawner)
    {
        ObstacleSpawner = obstacleSpawner;
        ObstacleSpawner.SpawnObstaclesOnTile(this);
    }

    
}
