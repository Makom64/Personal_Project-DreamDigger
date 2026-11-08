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
    }

    private void FixedUpdate()
    {
        _rigidbody2D.linearVelocityX =  _moveDirection.x * _moveSpeed;
    }

    private void Update()
    {
        Debug.Log(_rigidbody2D.linearVelocity);
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        // 여기서 입력값 x 속도
        // 입력값 = x 기준 좌우 방향
        _moveDirection = ctx.ReadValue<Vector2>();
    }

    private void OnDisable()
    {
        _moveInput.action.Disable(); // 입력 비활성화
    }

    private void CacheComponents()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
}
