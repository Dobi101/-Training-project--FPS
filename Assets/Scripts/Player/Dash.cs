using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement _playerMovement;

    [SerializeField]
    private float _dashBoost;
    [SerializeField]
    [Range(0f, 1f)]
    private float _dashFriction;
    [SerializeField]
    private float _dashLimitation;
    [SerializeField]
    private float _dashTimer;

    private Rigidbody _rigidbody;

    private bool _isDash;

    private Vector3 _direction;

    private float _dashTimerInside = 0;

    

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _playerMovement = GetComponent<PlayerMovement>();

    }

    private void Update()
    {
        if (!_playerMovement.isGrounded && Input.GetKeyDown(KeyCode.LeftControl) && !_isDash && _dashTimerInside==0)
        {
            if(_playerMovement.movementDirection != Vector3.zero)
            {
                _direction = _playerMovement.movementDirection;
                StartSlide();
            }
            else
            {
                _direction = transform.forward;
                StartSlide();
            }
        }
        if(_dashTimerInside != 0)
        {
            if (_dashTimerInside <= 0)
            {
                _dashTimerInside = 0;
            }
            else
            {
                _dashTimerInside -= 1 * Time.deltaTime;
            }
            
        }
        
    }

    private void FixedUpdate()
    {
        if (_isDash)
        {
            _rigidbody.AddForce(_direction * _dashFriction, ForceMode.Acceleration);
            if (_rigidbody.velocity.magnitude >= (_direction * _dashLimitation).magnitude)
            {
                EndSlide();
            }
        }
    }
    private void StartSlide()
    {
        _isDash = true;
        _rigidbody.AddForce(_direction * _dashBoost, ForceMode.Impulse);
        _dashTimerInside = _dashTimer;
        
    }

    private void EndSlide()
    {
        _isDash = false;
    }


}
