using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed; // 이동 속도
    [SerializeField] private InputActionReference _moveInput; // 이동 입력
    [SerializeField] private InputActionReference _interactionInput; // 상호작용 입력
    [SerializeField] private Tilemap _tilemap; // 타일맵
    private Rigidbody2D _rigidbody2D;
    private Vector2 _moveDirection; // 움직이는 방향
    private Vector3Int _currentPosition; // 플레이어 현재 좌표
    private Vector3Int _interactionPosition; // 상호작용 목표 좌표

    private void Awake()
    {
        CacheComponents();
    }

    private void OnEnable()
    {
        _moveInput.action.Enable(); // 이동 입력 활성화
        _moveInput.action.performed += OnMove;
        _moveInput.action.canceled += OnStopMove;
        _interactionInput.action.Enable(); // 상호작용 입력 활성화
        _interactionInput.action.performed += PlayerPositionInput;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    
    private void OnDisable()
    {
        _moveInput.action.performed -= OnMove;
        _moveInput.action.canceled -= OnStopMove;
        _moveInput.action.Disable(); // 이동 입력 비활성화
        _interactionInput.action.Disable(); // 상호작용 입력 비활성화
    }

    private void CacheComponents()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
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

    // 입력값으로 플레이어 현재 위치 구하기
    private void PlayerPositionInput(InputAction.CallbackContext ctx)
    {
        Debug.Log($"플레이어의 셀 좌표 : {ReadPlayerCellPosition()}");
    }
    
    private void ReadInteractionPos(InputAction.CallbackContext ctx)
    {
    }

    // 플레이어 월드 좌표를 셀 좌표로
    public Vector3Int ReadPlayerCellPosition()
    {
        _currentPosition = _tilemap.WorldToCell(transform.position);
        return _currentPosition;
    }

    /*public Vector3Int ReadInteractionPosition()
    {
        _interactionPosition.x = 
    }*/
}
