using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum CanvasType
{
    Main,
    Info,
    GameUi,
    Pausemenu,
    SuccessfulFinalScreen,
    FailedFinalScreen,
    CancelUi,
}
public class UiManager : Singltone<UiManager>
{
    [SerializeField] private CanvasType _initScreen;
    
    private List<CanvasController> _canvasControllers;

    private CanvasController _lastCanvas;
    private CanvasController _currentCanvas;


    public override void Awake()
    {
        base.Awake();

        _canvasControllers = GetComponentsInChildren<CanvasController>().ToList();
        _canvasControllers.ForEach(canvasController => canvasController.gameObject.SetActive(false));

        SwitchCanvas(_initScreen);
    }

    public void SwitchCanvas(CanvasType canvasType)
    {

        if (_lastCanvas != null)
        {
            _lastCanvas.gameObject.SetActive(false);
        }

        CanvasController desiredCanvas = _canvasControllers.Find(canvas => canvas.CanvasType == canvasType);

        if (desiredCanvas != null)
        {
            desiredCanvas.gameObject.SetActive(true);
            _lastCanvas = desiredCanvas;
        }

    }
}
