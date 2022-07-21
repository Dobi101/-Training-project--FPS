using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[AddComponentMenu("Control Script/ PlayerController")]
public class PlayerController : MonoBehaviour
{
    [Header("Components")] 
    [SerializeField]
    private Transform _headTransform;

    [Header("Mouse settings")]
    [SerializeField]
    [Range(10f, 200f)]
    private float _mouseSpeed = 20f;
    [SerializeField]
    [Range(45f, 90f)]
    private float _extremeAngle = 45f;

    [Header("Movement settings")]
    [SerializeField]
    [Range(1f, 100f)]
    private float _movementSpeed = 10f;
    [SerializeField]
    [Range(0f, 100f)]
    private float _acceleration = 10f;
    [SerializeField]
    [Range(0f, 90f)]
    [Tooltip("Максимальная наклонная поверхность для движения")]
    private float _slopeAngle = 45f;

    [Header("Jump settings")]
    [SerializeField]
    [Range(0f, 20f)]
    private float _jumpHeight = 1f;
    [SerializeField]
    [Range(0f, 1f)]
    [Tooltip("Возможность движения в прыжке")]
    private float _accelerationJump = 0.1f;
    [SerializeField]
    [Range(0,5)]
    private int _jumpsMaxCount = 2;
    [SerializeField]
    [Tooltip("Слои для проверки нахождения на земле")]
    private LayerMask _layerMask;

    private float _slideBoost;

    private float _rotationX = 0;
    private float _rotationY = 0;

    private Vector3 _desiredVelocity;

    private Rigidbody _rigidbody;
    private CapsuleCollider _collider;

    private bool _isGround;
    private bool _isSlide;
    private bool _prevSlideState;

    private int _jumpsCount;
    

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
        Cursor.visible = false;
        _jumpsCount = _jumpsMaxCount;
    }
    private void Update()
    {
        HandleMouse();
        CheckGround();
        HandleMovement();
    }

    private void FixedUpdate()
    {
        PhysicalVelocity();
    }
    private void OnCollisionStay(Collision collision)
    {
        _isGround = false;
        foreach (var contact in collision.contacts)
        {
           if(Vector3.Angle(transform.up, contact.normal) <= _slopeAngle)
            {
                _isGround = true;
                return;
            }
        }   
    }
    private void CheckGround()
    {
        if (_isGround)
        {
            if(_jumpsCount != _jumpsMaxCount)
            {
                _jumpsCount = _jumpsMaxCount;
            }
        }
        else
        {
            if(_jumpsCount == _jumpsMaxCount)
            {
                _jumpsCount = _jumpsMaxCount - 1;
            }
        }
    }
    private void HandleMouse()
    {
        Vector2 deltaMouse = new Vector2(Input.GetAxis("Mouse X"),
            Input.GetAxis("Mouse Y"));

        _rotationY = deltaMouse.x * _mouseSpeed;
        _rotationX -= deltaMouse.y * _mouseSpeed;
        _rotationX = Mathf.Clamp(_rotationX, -_extremeAngle, _extremeAngle);

        transform.Rotate(transform.up, _rotationY);
        _headTransform.localEulerAngles = Vector3.right * _rotationX;

    }

    private void HandleMovement()
    {
            Vector3 movementDirection = transform.right * Input.GetAxis("Horizontal")
            + transform.forward * Input.GetAxis("Vertical");
            if (movementDirection.magnitude > 1f)
            {
                movementDirection = movementDirection.normalized;
            }
            if (!_isSlide)
            {
                _desiredVelocity = movementDirection * _movementSpeed;
            }
           
        
        
        if((Input.GetKeyDown(KeyCode.Space) && _isGround) || (Input.GetKeyDown(KeyCode.Space) && _jumpsCount < _jumpsMaxCount && _jumpsCount > 0)) 
        {     
        
            _jumpsCount--;
            float g = Mathf.Abs(Physics.gravity.y);
            float verticalVelocity = Mathf.Sqrt(2 * _jumpHeight * g) ;
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            _rigidbody.AddForce(Vector3.up * _rigidbody.mass * verticalVelocity, ForceMode.Impulse);
        }
    }


    private void PhysicalVelocity()
    {
        Vector3 currentVelocity = _rigidbody.velocity;
        float yVelocity = currentVelocity.y;
        currentVelocity.y = 0;
        if (_desiredVelocity != currentVelocity)
        {
            float acceleration = ( _isGround ? _acceleration : _acceleration * _accelerationJump);

            float interpolation = acceleration * Time.deltaTime / 
                (_desiredVelocity - currentVelocity).magnitude;
            currentVelocity = Vector3.Lerp(currentVelocity, _desiredVelocity, interpolation);
        }
        currentVelocity.y = yVelocity;
        _rigidbody.velocity = currentVelocity;
        _isGround = false;
    }
}
