using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed; // 이동 속도
    [SerializeField] private InputActionReference _moveInput; // 이동 입력
    [SerializeField] private InputActionReference _interactionInput; // 상호작용 입력

    private Rigidbody2D _rigidbody2D;
    private Vector2 _moveDirection; // 움직이는 방향
    private Vector2 _playerDirection; // 플레이어 방향
    private Vector2Int _interactionPosition; // 상호작용 대상 지점

    private void Awake()
    {
        CacheComponents();
        _interactionPosition = new Vector2Int();
    }

    private void OnEnable()
    {
        _moveInput.action.Enable(); // 이동 입력 활성화
        _interactionInput.action.Enable(); // 상호작용 입력 활성화
        _moveInput.action.performed += OnMove;
        _moveInput.action.performed += PlayerDirectionInput;
        _moveInput.action.canceled += OnStopMove;
        _interactionInput.action.performed += ReadInteractionPos;
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
    }

    private void MovePlayer()
    {
        _rigidbody2D.linearVelocityX =  _moveDirection.x * _moveSpeed;
    }

    private void OnStopMove(InputAction.CallbackContext ctx)
    {
        _moveDirection = Vector2.zero;
    }

    // 입력값으로 플레이어 방향 구하기
    private void PlayerDirectionInput(InputAction.CallbackContext ctx)
    {
        _playerDirection.x = ctx.ReadValue<Vector2>().x;
        _playerDirection.y = ctx.ReadValue<Vector2>().y;
    }
    
    private void ReadInteractionPos(InputAction.CallbackContext ctx)
    {
        _interactionPosition.x = Convert.ToInt32(transform.position.x + _playerDirection.x);
        _interactionPosition.y = Convert.ToInt32(transform.position.y + _playerDirection.y);
        Debug.Log($"플레이어 현재 위치 : {transform.position}");
        Debug.Log($"상호작용 대상 지점 : {_interactionPosition.x}, {_interactionPosition.y}");
    }

    private void DetectTile(InputAction.CallbackContext ctx)
    {
    }
}
