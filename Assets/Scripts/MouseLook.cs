using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1, 
        MouseY = 2

    }

    [SerializeField] private RotationAxes _axes = RotationAxes.MouseXAndY;

    [SerializeField] private float _sensitivityHorizontal = 9.0f;
    [SerializeField] private float _sensitivityVertical = 9.0f;
    [SerializeField] private float _maxVertical = 45.0f;
    [SerializeField] private float _minVertical = -45.0f;

    private float _rotationX = 0;
    private float _rotationY = 0;
    private float _delta = 0;

    private void Update()
    {
        if (_axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * _sensitivityHorizontal, 0);
        }
        else if (_axes == RotationAxes.MouseY)
        {
            _rotationX -= Input.GetAxis("Mouse Y") * _sensitivityVertical;
            _rotationX = Mathf.Clamp(_rotationX, _minVertical, _maxVertical);

            _rotationY = transform.localEulerAngles.y;
            transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);

        }
        else
        {
            _rotationX -= Input.GetAxis("Mouse Y") * _sensitivityVertical;
            _rotationX = Mathf.Clamp(_rotationX, _minVertical, _maxVertical);

            _delta = Input.GetAxis("Mouse X") * _sensitivityHorizontal;
            _rotationY = transform.localEulerAngles.y + _delta;

            transform.localEulerAngles = new Vector3(_rotationX, _rotationY, 0);
        }


    }
}
