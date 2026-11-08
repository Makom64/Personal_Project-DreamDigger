using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed; // 이동 속도
    [SerializeField] private InputActionReference _moveInput; // 이동 입력

    private Rigidbody2D _rigidbody2D;
    private Vector2 _moveDirection;

    private void Awake()
    {
        CacheComponents();
    }

    private void OnEnable()
    {
        _moveInput.action.Enable(); // 입력 활성화
        _moveInput.action.performed += OnMove;
        _moveInput.action.canceled += OnStopMove;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    
    private void OnDisable()
    {
        _moveInput.action.performed -= OnMove;
        _moveInput.action.canceled -= OnStopMove;
        _moveInput.action.Disable();
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
}
