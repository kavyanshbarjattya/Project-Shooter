using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player_Movement : MonoBehaviour
{
    [SerializeField] private FixedJoystick _joystick;
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _deadZone = 0.05f; // Optional dead zone for tiny inputs
    [SerializeField] private SpriteRenderer[] _playerBody;
    [SerializeField] Animator _playerAnim;

    private Rigidbody2D _rb;
    private readonly Vector2 _zeroVector = Vector2.zero; // Avoid allocation
    private Vector2 _moveInput;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerAnim = GetComponent<Animator>();
    }

    void Update()
    {
        _moveInput.x = _joystick.Horizontal;
        _moveInput.y = _joystick.Vertical;
    }

    void FixedUpdate()
    {
        // Apply velocity only if input is significant
        if (_moveInput.sqrMagnitude > _deadZone * _deadZone)
        {
            _rb.velocity = _moveInput.normalized * _movementSpeed;
            PlayerFlip();
            _playerAnim.SetBool("Is_Run", true);
        }
        else if (_rb.velocity != _zeroVector)
        {
            _rb.velocity = _zeroVector; // Avoid unnecessary assignment
            _playerAnim.SetBool("Is_Run", false);
        }
    }
    private void PlayerFlip()
    {
        float h = _moveInput.x;
        foreach (SpriteRenderer p in _playerBody)
        {
            if (h < 0)
                p.flipX = true;
            else if (h > 0)
                p.flipX = false;
        }
    }
}
