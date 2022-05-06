using System.Collections.Generic;

public class PauseManager : IPauseHandler
{
    private List<IPauseHandler> _pauseHandlers = new List<IPauseHandler>();

    public bool IsPaused { get; private set; } = false;

    public void Register(IPauseHandler pauseHandler)
    {
        _pauseHandlers.Add(pauseHandler);
    }

    public void UnRegister(IPauseHandler pauseHandler)
    {
        _pauseHandlers.Remove(pauseHandler);
    }

    public void SetPause(bool isPaused)
    {
        IsPaused = isPaused;
        foreach (IPauseHandler item in _pauseHandlers)
        {
            item.SetPause(isPaused);
        }
    }
}
