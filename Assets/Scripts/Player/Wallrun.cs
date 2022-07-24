using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRun : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement _playerMovement;
    [SerializeField]
    private Transform _rotaitor;

    [SerializeField] 
    private float _wallRunGravity;
    [SerializeField]
    private float _wallRunSpeed;
    [SerializeField] 
    private float _wallRunJumpForce;
    [SerializeField]
    private Rigidbody _rigidbody;

    private float _wallDistance = 1f;

    private bool _wallLeft; 
    private bool _wallRight;

    private RaycastHit _leftWallHit;
    private RaycastHit _rightWallHit;

    private Vector3 _forward;
    

    private bool _wallRun;
    private bool _jump;
    private void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _wallRun)
        {
            _jump = true;
        }
    }
    private void FixedUpdate()
    {
        if (_wallRun)
        {
            _rigidbody.AddForce(_forward * _wallRunSpeed, ForceMode.Acceleration);
            _rigidbody.AddForce(Vector3.down * _wallRunGravity, ForceMode.Acceleration);

            if (_jump)
            {
                Jump();
            }
        }
        
        
    }

    private void Jump()
    {
        if (_wallLeft)
        {
            Vector3 wallRunJumpDirection = transform.up + _leftWallHit.normal;
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            _rigidbody.AddForce(wallRunJumpDirection * _wallRunJumpForce, ForceMode.Impulse);
        }
        else if (_wallRight)
        {
            Vector3 wallRunJumpDirection = transform.up + _rightWallHit.normal;
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            _rigidbody.AddForce(wallRunJumpDirection * _wallRunJumpForce, ForceMode.Impulse);
        }
        StopWallRun();
        _jump = false;
    }
    public void TryStartWallRun()
    {
        if (!_playerMovement.isGrounded)
        {
            _playerMovement.StartWallRun();
            CheckWall();
            if (_wallLeft)
            {

                _rotaitor.transform.rotation = Quaternion.FromToRotation
                    (_rotaitor.transform.forward, _leftWallHit.normal) * _rotaitor.transform.rotation;
                transform.rotation = Quaternion.FromToRotation(transform.forward, -_rotaitor.transform.right) * transform.rotation;
                transform.rotation = Quaternion.FromToRotation(transform.up, Vector3.up) * transform.rotation;
                _forward = transform.forward;
  
            }
            else if(_wallRight)
            {
                _rotaitor.transform.rotation = Quaternion.FromToRotation
                    (_rotaitor.transform.forward, _rightWallHit.normal) * _rotaitor.transform.rotation;
                transform.rotation = Quaternion.FromToRotation(transform.forward, _rotaitor.transform.right) * transform.rotation;
                transform.rotation = Quaternion.FromToRotation(transform.up, Vector3.up) * transform.rotation;
                _forward = transform.forward;
            }
            _rigidbody.useGravity = false;
            _wallRun = true;
            
        }
    }

    public void StopWallRun()
    {
        _wallRun=false;
        _rigidbody.useGravity = true;
        _playerMovement.EndWallRun();
    }

    private void CheckWall()
    {
        _wallLeft = Physics.Raycast(transform.position, -transform.right, out _leftWallHit, _wallDistance);
        _wallRight = Physics.Raycast(transform.position, transform.right, out _rightWallHit, _wallDistance);
    }
}
