using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hookshot : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement _playerMovement;
    [SerializeField]
    private WallRun _wallRun;
    [SerializeField]
    private Transform _firePoint;

    [SerializeField]
    private float _hookshotSpeedMultiplier = 2f;
    [SerializeField]
    private float _hookshotSpeedMin = 10f; 
    [SerializeField]
    private float _hookshotSpeedMax = 50f;
    [SerializeField]
    private float _distanceStopHookshot = 1f;
    [SerializeField]
    private float _maxDistance = 100f;
    [SerializeField]
    private LayerMask _layerMask;

    private Camera _camera;
    private Rigidbody _rigidbody;
    private LineRenderer _lineRenderer;

    private Vector3 _hookshotPosition; 
    private Vector3 _hookshotDirection; 

    private bool _isHookshot;
    

    private void Start()
    {
        _camera = GameObject.FindObjectOfType<Camera>().GetComponent<Camera>();
        _playerMovement = GetComponent<PlayerMovement>();
        _rigidbody = GetComponent<Rigidbody>();
        _wallRun = GetComponent<WallRun>();
        _lineRenderer = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        HandleHookshotStart();
        
    }

    private void FixedUpdate()
    {
        HookshotMovement();
    }

    private void LateUpdate()
    {
        DrawRope();
    }
    private void HandleHookshotStart()
    {
        if (Input.GetKeyDown(KeyCode.F) && !_isHookshot)
        {
            if(Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit raycastHit, _maxDistance, _layerMask))
            {
                // добавить проверку на объект
                _hookshotPosition = raycastHit.point;
                if (_playerMovement.wallRun)
                {
                    _wallRun.StopWallRun();
                }
                _isHookshot = true;
                _rigidbody.velocity = Vector3.zero;
                _rigidbody.useGravity = false;
                _playerMovement.StartHookshot();
                
            }
        }
        else if(Input.GetKeyDown(KeyCode.F) && _isHookshot)
        {
            _isHookshot = false;
            _rigidbody.useGravity = true;
            _playerMovement.EndHookshot();
        }
        if(Input.GetKeyDown(KeyCode.Space) && _isHookshot)
        {
            _isHookshot = false;
            _rigidbody.useGravity = true;
            _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, 0, _rigidbody.velocity.z);
            _rigidbody.AddForce(transform.up * _playerMovement.jumpForce, ForceMode.Impulse);
            _playerMovement.EndHookshot();
        }
        if (Vector3.Distance(transform.position, _hookshotPosition) < _distanceStopHookshot)
        {
            _isHookshot = false;
            _rigidbody.useGravity = true;
            _playerMovement.EndHookshot();
        }
    }

    private void HookshotMovement()
    {
        if (_isHookshot)
        {
            _hookshotDirection = (_hookshotPosition - transform.position).normalized;

            float hookshotSpeed = Mathf.Clamp(Vector3.Distance(transform.position, _hookshotPosition), _hookshotSpeedMin, _hookshotSpeedMax);

            _rigidbody.AddForce(_hookshotDirection * hookshotSpeed * _hookshotSpeedMultiplier, ForceMode.Acceleration);
        }
       

    }

    private void DrawRope()
    {
        if (_isHookshot)
        {
            _lineRenderer.SetPosition(0, _firePoint.position);
            _lineRenderer.SetPosition(1, _hookshotPosition);
        }
        if (!_isHookshot)
        {
            _lineRenderer.SetPosition(0, _firePoint.position);
            _lineRenderer.SetPosition(1, _firePoint.position);
        }
    }
}
