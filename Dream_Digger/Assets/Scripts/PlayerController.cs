using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed; // 이동 속도
    [SerializeField] private Tilemap _tilemap; // 타일맵
    
    [SerializeField] private InputActionReference _moveInput; // 이동 입력
    [SerializeField] private InputActionReference _interactionInput; // 상호작용 입력
    [SerializeField] private InputActionReference _verticalInput; // 상하입력
    [SerializeField] private InputActionReference _horizontalInput; // 좌우입력

    private int _bitePoint; // 플레이어 블록 파괴력
    private Rigidbody2D _rigidbody2D;
    private Vector2 _moveDirection; // 움직이는 방향
    private Vector3Int _currentPosition; // 플레이어 현재 좌표
    private Vector3Int _interactDirection; // 상호작용 방향
    private Vector3Int _initialDirection;

    // ------------------------- 생명주기
    private void Awake()
    {
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
    
    private void OnDisable()
    {
        UnbindInputEnable();
        UnbindInputActionPerformed();
        UnbindInputActionCanceled();
    }

    // ---------------------------------- private 메서드
    private void CacheComponents()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void BindInputEnable()
    {
        _moveInput.action.Enable();
        _interactionInput.action.Enable();
        _verticalInput.action.Enable();
        _horizontalInput.action.Enable();
    }

    private void UnbindInputEnable()
    {
        _moveInput.action.Disable();
        _interactionInput.action.Disable();
        _verticalInput.action.Disable();
        _horizontalInput.action.Disable();
    }
    
    private void BindInputActionPerformed()
    {
        _moveInput.action.performed += OnMove;
        _interactionInput.action.performed += OnTempInteract;
        _verticalInput.action.performed += OnVerticalDirection;
        _horizontalInput.action.performed += OnHorizontalDirection;
    }

    private void UnbindInputActionPerformed()
    {
        _moveInput.action.performed -= OnMove;
        _interactionInput.action.performed -= OnTempInteract;
        _verticalInput.action.performed -= OnVerticalDirection;
        _horizontalInput.action.performed -= OnHorizontalDirection;
    }

    private void BindInputActionCanceled()
    {
        _moveInput.action.canceled += OnStopMove;
        _verticalInput.action.canceled += OnStopVerticalDirection;
        _horizontalInput.action.canceled += OnStopHorizontalDirection;
    }

    private void UnbindInputActionCanceled()
    {
        _moveInput.action.canceled -= OnStopMove;
        _verticalInput.action.canceled -= OnStopVerticalDirection;
        _horizontalInput.action.canceled -= OnStopHorizontalDirection;
    }
    
    private void OnMove(InputAction.CallbackContext ctx)
    {
        _moveDirection.x = ctx.ReadValue<Vector2>().x;
        Debug.Log(ctx.ReadValue<Vector2>());
    }

    private void MovePlayer()
    {
        _rigidbody2D.linearVelocityX =  _moveDirection.x * _moveSpeed;
    }

    private void OnStopMove(InputAction.CallbackContext ctx)
    {
        _moveDirection = Vector2.zero;
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
    }

    private void OnStopVerticalDirection(InputAction.CallbackContext ctx)
    {
        // 좌우 키가 입력 중이었다면 그 방향으로
        if (_horizontalInput.action.IsPressed())
        {
            _interactDirection = ChangeToCellPosition(_horizontalInput.action.ReadValue<Vector3>());
        }
    }

    private Vector3Int ChangeToCellPosition(Vector3 worldPosition)
    {
        return _tilemap.WorldToCell(worldPosition);
    }

    private void OnTempInteract(InputAction.CallbackContext ctx)
    {
        GetPlayerCellPosition();
        _tilemap.SetTile(_currentPosition + _interactDirection, null);
    }

    private void BiteBlock(InputAction.CallbackContext ctx)
    {
        // 현재 위치에서 상호작용 방향으로 부술 수 있는 타일이 있는지 검사, 아니라면 종료
        if (_tilemap.HasTile(_currentPosition + _interactDirection) == false ||
            _tilemap.GetTile<BreakableTile>(_currentPosition + _interactDirection) == false)
        {
            return;
        }
        else
        {
            // 부술 수 있는 타일이면 타일 '내구도'에서 '파괴력'만큼 차감
            _tilemap.GetTile<BreakableTile>(_currentPosition + _interactDirection).BiteTile(_currentPosition + _interactDirection, _bitePoint);
        }
        
    }
}
