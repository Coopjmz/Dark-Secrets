using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
sealed class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private float _jumpForce = 10f;

    private Rigidbody2D _rigidbody;

    private float _horizontalDirection;
    private bool _isJumping;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        UpdateInput();
    }

    private void UpdateInput()
    {
        _horizontalDirection = Input.GetAxisRaw("Horizontal");
        _isJumping |= Input.GetButtonDown("Jump");
    }

    private void FixedUpdate()
    {
        if (_horizontalDirection != 0f)
            Move();

        if (_isJumping)
        {
            _isJumping = false;
            Jump();
        }
    }

    private void Move()
    {
        _rigidbody.velocity = new Vector2(_horizontalDirection * _speed, _rigidbody.velocity.y);

        if (_horizontalDirection * transform.localScale.x < 0f)
            transform.localScale = new Vector3(_horizontalDirection * Mathf.Abs(transform.localScale.x), transform.localScale.y);
    }

    private void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }
}