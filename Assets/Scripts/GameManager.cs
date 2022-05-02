using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singltone<GameManager>
{
    [SerializeField] private int _gamesAvailable;
    [Tooltip("Count of points for finish")]
    [SerializeField] private int _maxPoints;
    [SerializeField] private int _displayDelay;
    private int _currentPoints;

    public PauseManager pauseManager;

    public int GamesAvailable
    {
        get => _gamesAvailable;
        private set { _gamesAvailable = value; }
    }

    public int MaxPoints
    {
        get => _maxPoints;
        private set { _maxPoints = value; }
    }

    public int CurrentPoints
    {
        get => _currentPoints;
        private set { _currentPoints = value; }
    }


    public override void Awake()
    {
        base.Awake();

        pauseManager = new PauseManager();
    }

    public void SetPause(bool isPaused)
    {
        pauseManager.SetPause(isPaused);
    }

    public void StartGame()
    {
        if (_gamesAvailable < 0) return;
        _gamesAvailable--;
        CurrentPoints = 0;
        pauseManager.SetPause(false);
    }

    public void ShowFinalScreen()
    {
        if (_currentPoints > 0)
        {
            StartCoroutine(ShowScreen(_displayDelay, CanvasType.SuccessfulFinalScreen));
        }
        else
        {
            StartCoroutine(ShowScreen(_displayDelay, CanvasType.FailedFinalScreen));
        }
    }

    public void ChangePoints(int points)
    {
        CurrentPoints += points;
    }

    private IEnumerator ShowScreen(int delay, CanvasType canvasType)
    {
        yield return new WaitForSeconds(delay);
        UiManager.Instance.SwitchCanvas(canvasType);
    }

}
