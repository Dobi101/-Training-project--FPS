using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent (typeof(CapsuleCollider))]
public class PlayerMovement : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField]
    [Range(0f, 100f)]
    private float _speed = 100f;
    [SerializeField]
    [Range(0f, 100f)]
    private float _multiplier = 10f;

    [Header("Jump settings")]
    [SerializeField]
    [Range(0f, 100f)]
    private float _jumpForce = 5f;
    [SerializeField]
    [Range(0f, 1f)]
    [Tooltip("Возможность движения в прыжке")]
    private float _accelerationJump = 0.1f;

    [Header("Drag")]
    [SerializeField]
    [Range(0f, 100f)]
    [Tooltip("Сопротивление на земле")]
    private float _dragOnGround = 6f;
    [SerializeField]
    [Range(0f, 100f)]
    [Tooltip("Сопротивление в воздухе")]
    private float _dragOnAir = 2f;

    [Header("Ground Detection")]
    [SerializeField]
    private LayerMask _groundMask;
    [SerializeField]

    private float _groundDistance = 0.4f;

    private Vector3 _movementDirection;
    private Vector3 _slopeDirection;

    private int _jumpCount;

    private Rigidbody _rigidbody;
    private CapsuleCollider _collider;
    private WallRun _wallRunComponent;

    private bool _isGrounded;
    private bool _doubleJump;
    private bool _wallRun;
    private bool _isHookshot;

    public bool wallRun => _wallRun;
    public bool isGrounded => _isGrounded;
    public Vector3 movementDirection => _movementDirection;

    public float jumpForce => _jumpForce;

    private void Start()
    {
        _collider = GetComponent<CapsuleCollider>();
        _rigidbody = GetComponent<Rigidbody>();
        _wallRunComponent = GetComponent<WallRun>();
        _rigidbody.freezeRotation = true;
        _jumpCount = 2;
    }

    private void Update()
    {
        CheckGround();

        if (!_wallRun || !_isHookshot)
        {
            Handle();
            ControlDrag();
        }
    }

    private void FixedUpdate()
    {
        if (!_wallRun || !_isHookshot)
        {
            if (_movementDirection != Vector3.zero)
            {
                Movement();
            }
        }
        


    }

    public void StartHookshot()
    {
        _isHookshot = true;
    }

    public void EndHookshot()
    {
        _isHookshot = false;
        _jumpCount = 2;
    }
    public void StartWallRun()
    {
        _wallRun = true;
    }

    public void EndWallRun()
    {
        _wallRun = false;
        _jumpCount = 2;
    }
    private bool OnSlope()
    {
        RaycastHit slopeHit;

        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, _collider.height / 2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                _slopeDirection = Vector3.ProjectOnPlane(_movementDirection, slopeHit.normal);
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    private void CheckGround()
    {
        _isGrounded = Physics.CheckSphere(transform.position + Vector3.down,
            _groundDistance, _groundMask);

        if (_isGrounded)
        {
            if (_jumpCount != 2)
            {
                _jumpCount = 2;
            }
        }
        else
        {
            if (_jumpCount == 2)
            {
                _jumpCount = 1;
            }
        }


        if (_wallRun && _isGrounded)
        {
            _wallRunComponent.StopWallRun();
        }

    }

    
    private void Handle()
    {
        _movementDirection = transform.right * Input.GetAxisRaw("Horizontal")
            + transform.forward * Input.GetAxisRaw("Vertical");
        if (_movementDirection.magnitude > 1f)
        {
            _movementDirection = _movementDirection.normalized;
        }


        if (Input.GetKeyDown(KeyCode.Space)&&_isGrounded)
        {
            Jump();
        }
        else if (Input.GetKeyDown(KeyCode.Space) && _jumpCount == 1 && !_isGrounded)
        {
            _doubleJump = true;
            Jump();
        }
    }

    private void ControlDrag()
    {
        if (_isGrounded)
        {
            _rigidbody.drag = _dragOnGround;
        }
        else
        {
            if (_rigidbody.velocity.y < 0)
            {
                _rigidbody.drag = 0;
            }
            else
            {
                _rigidbody.drag = _dragOnAir;
            }
            
        }
    }

    private void Movement()
    {
        if (_isGrounded && !OnSlope())
        {
            _rigidbody.AddForce(_movementDirection * _speed * _multiplier, ForceMode.Acceleration);
        }
        else if (_isGrounded && OnSlope())
        {
            _rigidbody.AddForce(_slopeDirection * _speed * _multiplier, ForceMode.Acceleration);
        }
        else if(!_isGrounded)
        {
            _rigidbody.AddForce(_movementDirection * _speed * _multiplier * _accelerationJump, ForceMode.Acceleration);
        }else if(!_isGrounded && _doubleJump)
        {
            _rigidbody.AddForce(_movementDirection * _speed * _multiplier , ForceMode.Acceleration);
            _doubleJump = false;
        }
        
    }

    private void Jump()
    {
        _jumpCount--;
        _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
        _rigidbody.AddForce(transform.up *_jumpForce, ForceMode.Impulse);
    }

}
