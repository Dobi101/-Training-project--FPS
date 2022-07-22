using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlatform : MonoBehaviour
{
    private Vector3 _startPosition;

    [SerializeField]
    private Transform _endPosition;
    [SerializeField]
    private float _speed;

    private Vector3 _moveToPosition;

    private void Start()
    {
        _startPosition = transform.position;
        _moveToPosition = _endPosition.position;
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards( transform.position, _moveToPosition, _speed * Time.deltaTime);
        if(transform.position == _endPosition.position)
        {
            _moveToPosition = _startPosition;
        }
        if (transform.position == _startPosition)
        {
            _moveToPosition = _endPosition.position;
        }
    }

   


}
