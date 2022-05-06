using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private float _maxRoadSpeed;
    [SerializeField] private float _minRoadSpeed;

    private float _currentRoadSpeed = 0;
    private bool _isMoving = false;
    private PauseManager _pauseManager => GameManager.Instance.pauseManager;


    void Update()
    {
        if (_pauseManager.IsPaused || !_isMoving) return;

        transform.Translate(Vector3.back * _currentRoadSpeed * Time.deltaTime);
        if (_currentRoadSpeed < _maxRoadSpeed)
        {
            _currentRoadSpeed += Time.deltaTime * 0.2f;
        }
        
    }

    public void StartGame()
    {
        _currentRoadSpeed = _minRoadSpeed;
        _isMoving = true;
    }

    public void StopRoad()
    {
        _currentRoadSpeed = 0;
        _isMoving = false;
    }

    
}
