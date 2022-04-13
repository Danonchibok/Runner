using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Runner/New Obstacle", fileName = "NewObstacle")]
public class Obstacle : ScriptableObject
{
    public LinePosition linePosition;
    public ObstacleType obstacleType;
    public GameObject worldObjectPrefab;

}

public enum LinePosition
{
    Left = -1,
    Right = 1,
    Center = 0,
    Any = 2,
}

public enum ObstacleType
{
    Jump,
    Full,
}