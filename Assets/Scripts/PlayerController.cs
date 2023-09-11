using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
sealed class PlayerController : MonoBehaviour
{
    const float EPSILON = .001f;

    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _jumpForce = 80f;
    [SerializeField] private int _maxNumberOfJumps = 2;

    // Components
    private Rigidbody2D _rigidbody;
    private Animator _animator;

    // Input
    private float _horizontalDirection;
    private bool _jumpButtonPressed;
    private bool _attackButtonPressed;

    private int _numberOfJumps;

    private bool IsWalking => _horizontalDirection != 0f;
    private bool IsGrounded => Mathf.Abs(_rigidbody.velocity.y) < EPSILON;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        UpdateInput();
        UpdateAnimations();
    }

    private void UpdateInput()
    {
        _horizontalDirection = Input.GetAxisRaw("Horizontal");
        _jumpButtonPressed |= Input.GetButtonDown("Jump");
        _attackButtonPressed |= Input.GetButtonDown("Attack");
    }

    private void UpdateAnimations()
    {
        _animator.SetBool("isWalking", IsWalking);
        _animator.SetBool("isGrounded", IsGrounded);
        _animator.SetFloat("yVelocity", _rigidbody.velocity.y);
    }

    private void FixedUpdate()
    {
        if (IsWalking)
            Move();

        if (_numberOfJumps > 0 && _jumpButtonPressed)
        {
            --_numberOfJumps;
            Jump();
        }

        if (IsGrounded)
            _numberOfJumps = _maxNumberOfJumps;

        ResetInput();
    }

    private void Move()
    {
        _rigidbody.velocity = new Vector2(Mathf.Sign(_horizontalDirection) * _speed, _rigidbody.velocity.y);

        if (_horizontalDirection * transform.localScale.x < 0f)
            transform.localScale = new Vector3(_horizontalDirection * Mathf.Abs(transform.localScale.x), transform.localScale.y);
    }

    private void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }

    private void ResetInput()
    {
        _jumpButtonPressed = false;
        _attackButtonPressed = false;
    }
}