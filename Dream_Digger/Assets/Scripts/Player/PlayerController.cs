using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.Tilemaps;

public class PlayerController : SingletonBase<PlayerController>
{
    [SerializeField] private float _moveSpeed; // 이동 속도
    [SerializeField] private float _interactCoolTime;
    [SerializeField] private Tilemap _tilemap; // 타일맵
    
    [SerializeField] private InputActionReference _moveInput; // 이동 입력
    [SerializeField] private InputActionReference _flightInput; // 비행 입력
    [SerializeField] private InputActionReference _interactionInput; // 상호작용 입력
    [SerializeField] private InputActionReference _verticalInput; // 상하입력
    [SerializeField] private InputActionReference _horizontalInput; // 좌우입력
    
    private float _interactCoolTimer;
    private SpriteRenderer _renderer;
    private Rigidbody2D _rigidbody2D;
    private Vector2 _moveDirection; // 움직이는 방향
    private Vector3Int _currentPosition; // 플레이어 현재 좌표
    private Vector3Int _interactDirection; // 상호작용 방향
    private Vector3Int _initialDirection;
    private Animator _animator;
    
    public int _bitePoint; // 플레이어 블록 파괴력
    public PlayerState CurrentState;
    public int _memoryCount; // 획득 자원 수

    // ------------------------- 생명주기
    protected override void Awake()
    {
        base.Awake();
        CacheComponents();
        InitDirection();
    }

    private void OnEnable()
    {
        BindInputEnable();
        BindInputActionPerformed();
        BindInputActionCanceled();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void Update()
    {
        if (!_interactionInput.action.IsPressed())
        {
            return;
        }
        else
        {
            _interactCoolTimer += Time.deltaTime;
            if (_interactCoolTimer >= _interactCoolTime)
            {
                BiteBlock();
                _interactCoolTimer = 0;
            }
        }
    }
    
    private void OnDisable()
    {
        UnbindInputEnable();
        UnbindInputActionPerformed();
        UnbindInputActionCanceled();
    }

    // ---------------------------------- private 메서드
    private void CacheComponents()
    {
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void BindInputEnable()
    {
        _moveInput.action.Enable();
        _flightInput.action.Enable();
        _interactionInput.action.Enable();
        _verticalInput.action.Enable();
        _horizontalInput.action.Enable();
    }

    private void UnbindInputEnable()
    {
        _moveInput.action.Disable();
        _flightInput.action.Disable();
        _interactionInput.action.Disable();
        _verticalInput.action.Disable();
        _horizontalInput.action.Disable();
    }
    
    private void BindInputActionPerformed()
    {
        _moveInput.action.performed += OnMove;
        _flightInput.action.performed += OnFlight;
        _interactionInput.action.performed += OnInteraction;
        _verticalInput.action.performed += OnVerticalDirection;
        _horizontalInput.action.performed += OnHorizontalDirection;
    }

    private void UnbindInputActionPerformed()
    {
        _moveInput.action.performed -= OnMove;
        _flightInput.action.performed -= OnFlight;
        _interactionInput.action.performed -= OnInteraction;
        _verticalInput.action.performed -= OnVerticalDirection;
        _horizontalInput.action.performed -= OnHorizontalDirection;
    }

    private void BindInputActionCanceled()
    {
        _moveInput.action.canceled += OnStopMove;
        _flightInput.action.canceled += OnStopFlight;
        _interactionInput.action.canceled += OnStopInteraction;
        _verticalInput.action.canceled += OnStopVerticalDirection;
        _horizontalInput.action.canceled += OnStopHorizontalDirection;
    }

    private void UnbindInputActionCanceled()
    {
        _moveInput.action.canceled -= OnStopMove;
        _flightInput.action.canceled -= OnStopFlight;
        _interactionInput.action.canceled -= OnStopInteraction;
        _verticalInput.action.canceled -= OnStopVerticalDirection;
        _horizontalInput.action.canceled -= OnStopHorizontalDirection;
    }
    
    private void OnMove(InputAction.CallbackContext ctx)
    {
        _moveDirection.x = ctx.ReadValue<Vector2>().x;
        _animator.SetBool("isRunning", true);
        if (_moveDirection.x > 0.0f)
        {
            _renderer.flipX = false;
        }
        else if (_moveDirection.x < -0.0f)
        {
            Debug.Log("뒤집어짐");
            _renderer.flipX = true;
        }
    }
    
    private void OnStopMove(InputAction.CallbackContext ctx)
    {
        _animator.SetBool("isRunning", false);
        _moveDirection = Vector2.zero;
    }

    private void MovePlayer()
    {
        _rigidbody2D.linearVelocityX =  _moveDirection.x * _moveSpeed;
        if (_rigidbody2D.linearVelocityY > _moveSpeed)
        {
            _rigidbody2D.linearVelocityY = _moveSpeed;
        }
    }

    private void OnFlight(InputAction.CallbackContext ctx)
    {
        _rigidbody2D.gravityScale = - ctx.ReadValue<Vector2>().y;
        _animator.SetBool("isFlying", true);
    }

    private void OnStopFlight(InputAction.CallbackContext ctx)
    {
        _rigidbody2D.gravityScale = 1.0f;
        _animator.SetBool("isFlying", false);
    }
    
    private void GetPlayerCellPosition() // 플레이어 좌표를 셀좌표로 변환
    {
        _currentPosition = _tilemap.WorldToCell(transform.position);
    }

    private void InitDirection()
    {
        _initialDirection = new Vector3Int(1, 0, 0);
        _interactDirection = _initialDirection;
    }

    private void OnHorizontalDirection(InputAction.CallbackContext ctx)
    {
        _interactDirection = ChangeToCellPosition(ctx.ReadValue<Vector3>());
    }

    private void OnStopHorizontalDirection(InputAction.CallbackContext ctx)
    {
        // 상하 키가 입력 중이였다면 그 방향으로
        if (_verticalInput.action.IsPressed())
        {
            _interactDirection = ChangeToCellPosition(_verticalInput.action.ReadValue<Vector3>());
        }
    }

    private void OnVerticalDirection(InputAction.CallbackContext ctx)
    {
        _interactDirection = ChangeToCellPosition(ctx.ReadValue<Vector3>());
        if (ctx.ReadValue<Vector3>().y > 0)
        {
            _animator.SetBool("directionUp", true);
        }
        else if (ctx.ReadValue<Vector3>().y < 0)
        {
            _animator.SetBool("directionDown", true);
        }
    }

    private void OnStopVerticalDirection(InputAction.CallbackContext ctx)
    {
        // 좌우 키가 입력 중이었다면 그 방향으로
        if (_horizontalInput.action.IsPressed())
        {
            _interactDirection = ChangeToCellPosition(_horizontalInput.action.ReadValue<Vector3>());
        }

        if (_animator.GetBool("directionUp"))
        {
            _animator.SetBool("directionUp", false);
        }
        else if (_animator.GetBool("directionDown"))
        {
            _animator.SetBool("directionDown", false);
        }
    }

    private Vector3Int ChangeToCellPosition(Vector3 worldPosition)
    {
        return _tilemap.WorldToCell(worldPosition);
    }

    private void OnInteraction(InputAction.CallbackContext ctx)
    {
        _animator.SetBool("isBiting", true);
    }

    private void OnStopInteraction(InputAction.CallbackContext ctx)
    {
        _animator.SetBool("isBiting", false);
    }

    private void BiteBlock()
    {
        GetPlayerCellPosition();
        MapController.Instance.TakeBite(_interactDirection + _currentPosition, _bitePoint);
    }
}
