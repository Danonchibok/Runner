using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour, IPauseHandler
{
    [SerializeField] private float _maxRoadSpeed;
    private float _currentRoadSpeed = 0;
    private PauseManager _pauseManager => GameManager.Instance.pauseManager;

    private void Start()
    {
        _pauseManager.Register(this);
    }

    void Update()
    {
        transform.Translate(Vector3.back * _currentRoadSpeed * Time.deltaTime);    
    }

    public void StartGame()
    {
        _currentRoadSpeed = _maxRoadSpeed;
    }

    public void StopRoad()
    {
        _currentRoadSpeed = 0;
    }

    public void SetPause(bool isPaused)
    {
        if (isPaused)
        {
            _currentRoadSpeed = 0;
        }
        else
        {
            _currentRoadSpeed = _maxRoadSpeed;
        }
    }
}
