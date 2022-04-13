using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Image _progressBar;
    [SerializeField] private TextMeshProUGUI _pointCounter;
    private float _maxPoints;

    private float _collectedPoints;
    private float _progress;

    private void Start()
    {
        Init();
    }

    public void ChangeProgress(int progressCount)
    {
        _collectedPoints += progressCount;
        _pointCounter.text = _collectedPoints.ToString(); 
        _progress += progressCount / _maxPoints;
        StartCoroutine(FillProgressCorutine());
    }

    private IEnumerator FillProgressCorutine()
    {
        while (_progressBar.fillAmount <= _progress)
        {
            yield return new WaitForFixedUpdate();
            _progressBar.fillAmount = Mathf.Lerp(_progressBar.fillAmount, _progress + _progressBar.fillAmount, Time.deltaTime);
        }
    }
    public void Init()
    {
        _maxPoints = GameManager.Instance.MaxPoints;
        _progress = 0;
        _progressBar.fillAmount = _progress;
        _collectedPoints = 0;
        _pointCounter.text = _collectedPoints.ToString();
    }
       
}
