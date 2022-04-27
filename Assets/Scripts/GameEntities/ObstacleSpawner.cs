using System.Collections.Generic;
using System.Linq;
using UnityEngine;




public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private List<Obstacle> _obstacles;
    [SerializeField] private float _countObstacles;
    [SerializeField] private float _spaceBetweenObstacles;
    [SerializeField] private float _linesOffset;

    public float LinesOffset => _linesOffset;

    public GameObject SpawnObstaclesOnTile(RoadTile roadTile)
    {
        GameObject obstaclesContainer = new GameObject("Obstacles");
        obstaclesContainer.transform.SetParent(roadTile.transform);

        for (int i = 0; i < _countObstacles; i++)
        {
            LinePosition line = (LinePosition) Random.Range(-1,2);
            List<Obstacle> obstacles = _obstacles.Where(obs => obs.linePosition == line ||
                obs.linePosition == LinePosition.Any).ToList();

            GameObject obstacle = obstacles[Random.Range(0, obstacles.Count)].worldObjectPrefab;

            Vector3 position = new Vector3((int)line * _linesOffset, 0.5f, i * _spaceBetweenObstacles) + roadTile.transform.position;
            GameObject go = Instantiate(obstacle, position + obstacle.transform.position, obstacle.transform.rotation);
            go.transform.SetParent(obstaclesContainer.transform);
        }

        return obstaclesContainer;
    }
}
