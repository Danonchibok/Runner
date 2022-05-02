using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour, IPauseHandler
{
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _jumpHeight;

    private Vector3 _startPos;
    private Animator _animator;
    private ObstacleSpawner _obstacleSpawner;
    private Rigidbody _rigidbody;
    private Coroutine _movingCoroutine;

    public IntEvent OnRaisePoint;
    public VoidEvent OnLose;
    private PauseManager _pausedManager => GameManager.Instance.pauseManager;

    private float _startPosition;
    private float _endPosition;
    private bool _isMoving;
    private bool _grounded = true;
    private bool _gamePaused = false;

    private int _runAnim;
    private float _animSpeed;

  
    private void Start()
    {
        _startPos = transform.position;
        _pausedManager.Register(this);
        _obstacleSpawner = FindObjectOfType<ObstacleSpawner>();
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        _runAnim = Animator.StringToHash("IsRun");
        _animSpeed = _animator.speed;
    }

    void Update()
    {
        if (_gamePaused) return;

        Move();
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && _endPosition < _obstacleSpawner.LinesOffset)
        {
            MoveHorizontal(_horizontalSpeed);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && _endPosition > -_obstacleSpawner.LinesOffset)
        {
            MoveHorizontal(-_horizontalSpeed);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && _grounded)
        {
            _grounded = false;
            _rigidbody.AddForce(Vector3.up * _jumpHeight, ForceMode.Impulse);
        }

       
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
        _animator.SetBool(_runAnim, true);
    }
}
