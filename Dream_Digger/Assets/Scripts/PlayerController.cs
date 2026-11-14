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
    
    private Rigidbody2D _rigidbody2D;
    private Vector2 _moveDirection; // 움직이는 방향
    private Vector3Int _currentPosition; // 플레이어 현재 좌표
    private Vector3Int _interactDirection; // 상호작용 방향

    private void Awake()
    {
        CacheComponents();
    }

    private void OnEnable()
    {
        BindInputEnable();
        _moveInput.action.performed += OnMove;
        _moveInput.action.canceled += OnStopMove;
        _horizontalInput.action.performed += OnHorizontalDirection;
        _verticalInput.action.performed += OnVerticalDirection;
        _interactionInput.action.performed += OnTempInteract;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    
    private void OnDisable()
    {
        _moveInput.action.performed -= OnMove;
        _moveInput.action.canceled -= OnStopMove;
        UnbindInputEnable();
    }

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

    private void OnHorizontalDirection(InputAction.CallbackContext ctx)
    {
        _interactDirection = ChangeToCellPosition(ctx.ReadValue<Vector3>());
    }

    private void OnVerticalDirection(InputAction.CallbackContext ctx)
    {
        _interactDirection = ChangeToCellPosition(ctx.ReadValue<Vector3>());
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
}
