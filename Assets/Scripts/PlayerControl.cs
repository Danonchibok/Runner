using System.Collections;
using UnityEngine;

public class PlayerControl : MonoBehaviour, IPauseHandler
{
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _jumpHeight;
    [SerializeField] private Canvas _lossCanvas;

    private Vector3 _startPos;
    private Coroutine _movingCoroutine;

    public IntEvent OnRaisePoint;
    public VoidEvent OnLose;
    private PauseManager _pausedManager => GameManager.Instance.pauseManager;
    private PlayerInputSystem _inputs;
    private Animator _animator;
    private Rigidbody _rigidbody;
    private ObstacleSpawner _obstacleSpawner;

    private float _startPosition;
    private float _endPosition;
    private bool _isMoving;
    private bool _grounded = true;
    private bool _gamePaused = false;

    private int _runAnim;
    private int _lossAnim;
    private int _jumpAnim;
    private float _animSpeed;

  
    private void Start()
    {
        _startPos = transform.position;
        _pausedManager.Register(this);
        _inputs = GetComponent<PlayerInputSystem>();
        _obstacleSpawner = FindObjectOfType<ObstacleSpawner>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        _runAnim = Animator.StringToHash("IsRun");
        _jumpAnim = Animator.StringToHash("Jump");
        _lossAnim = Animator.StringToHash("Loss");
        _animSpeed = _animator.speed;
        InitMovement();
    }

   
    private void InitMovement()
    {
        _inputs.Jump += () =>
        {
            if (_grounded && !_gamePaused)
            {
                _grounded = false;
                _animator.SetTrigger(_jumpAnim);
                _rigidbody.AddForce(Vector3.up * _jumpHeight, ForceMode.Impulse);
            }
        };

        _inputs.RightDash += () =>
        {
            if (_endPosition < _obstacleSpawner.LinesOffset && !_gamePaused)
            {
                MoveHorizontal(_horizontalSpeed);
            }
        };

        _inputs.LeftDash += () =>
        {
            if (_endPosition > -_obstacleSpawner.LinesOffset && !_gamePaused)
            {
                MoveHorizontal(-_horizontalSpeed);  
            }
        };
    }

   
    void MoveHorizontal(float moveSpeed)
    {
        _startPosition = _endPosition;
        _endPosition += Mathf.Sign(moveSpeed) * _obstacleSpawner.LinesOffset;

        if (_isMoving) 
        {
            StopCoroutine(_movingCoroutine);
            _isMoving = false;
        }
        _movingCoroutine = StartCoroutine(MoveCoroutine(moveSpeed));
    }

    IEnumerator MoveCoroutine(float xVector)
    {
        _isMoving = true;
        while(Mathf.Abs(_startPosition - transform.position.x) < _obstacleSpawner.LinesOffset)
        {
            yield return new WaitForFixedUpdate();

            _rigidbody.velocity = new Vector3(xVector, _rigidbody.velocity.y, 0);

            float xPos = Mathf.Clamp(transform.position.x, Mathf.Min(_startPosition, _endPosition), Mathf.Max(_startPosition, _endPosition));
            transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
        }

        _rigidbody.velocity = Vector3.zero;
        transform.position = new Vector3(_endPosition, transform.position.y, transform.position.z);
        _isMoving = false;  
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            _grounded = true;
        }
        if (collision.collider.CompareTag("Point"))
        {
            OnRaisePoint.Raise(1);
        }
        if (collision.collider.CompareTag("Obstacle"))
        {
            OnLose.Raise();
            _lossCanvas.gameObject.SetActive(true);
            _animator.SetBool(_runAnim, false);
            _animator.SetTrigger(_lossAnim);
            _inputs.enabled = false;
        }
    }

    public void SetPause(bool isPaused)
    {
        _gamePaused = isPaused;
        if (isPaused)
        {
            _animator.speed = 0;
        }
        else
        {
            _animator.speed = _animSpeed;
        }
    }

    public void OnStartGame()
    {
        transform.position = _startPos;
        _endPosition = 0;
        _lossCanvas.gameObject.SetActive(false);
        _animator.SetBool(_runAnim, true);
        _inputs.enabled = true;
    }
}
