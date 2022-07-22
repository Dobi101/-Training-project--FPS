using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMovementPlatform : MonoBehaviour
{
    private Vector3 _startPosition;

    [SerializeField]
    private Transform _endPosition;
    [SerializeField]
    private float _speed;

    private Vector3 _moveToPosition;

    private bool _start;

    private void Start()
    {
        _startPosition = transform.position;
        _moveToPosition = _endPosition.position;
    }
    private void FixedUpdate()
    {
        if (_start)
        {
            transform.position = Vector3.MoveTowards(transform.position, _moveToPosition, _speed * Time.deltaTime);
            if (transform.position == _endPosition.position)
            {
                _moveToPosition = _startPosition;
                _start = false;
            }
            if (transform.position == _startPosition)
            {
                _moveToPosition = _endPosition.position;
                _start = false;
            }
        
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.parent = transform;
        _start = true;

    }

    private void OnCollisionExit(Collision collision)
    {
        collision.transform.parent = null;
    }


}
