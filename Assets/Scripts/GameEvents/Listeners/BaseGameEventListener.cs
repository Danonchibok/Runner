using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 
/// </summary>
/// <typeparam name="T">Any Type</typeparam>
/// <typeparam name="E">Game Event</typeparam>
/// <typeparam name="UER">Unity Event Response</typeparam>
public abstract class BaseGameEventListener<T, E, UER> : MonoBehaviour, 
    IGameEventListener<T> where E: BaseGameEvent<T> where UER : UnityEvent<T>
{
    [SerializeField] private E _gameEvent;
    [SerializeField] public UER _unityEventResponse;

    public E GameEvent
    {
        get => _gameEvent;
        set
        {
            _gameEvent = value;
        }
    }

    private void OnEnable()
    {
        if (_gameEvent == null) return;
        _gameEvent.RegisterListener(this);
    }

    private void OnDisable()
    {
        if (_gameEvent == null) return;
        _gameEvent.UnregisterListener(this);
    }

    public void OnEventRaised(T item)
    {
        if (_unityEventResponse == null) return;

        _unityEventResponse.Invoke(item);
    }
}
