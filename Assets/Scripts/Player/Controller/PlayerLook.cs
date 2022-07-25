using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
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

    private float _rotationX = 0;
    private float _rotationY = 0;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        Vector2 deltaMouse = new Vector2(Input.GetAxis("Mouse X"),
            Input.GetAxis("Mouse Y"));

        _rotationY = deltaMouse.x * _mouseSpeed;
        _rotationX -= deltaMouse.y * _mouseSpeed;
        _rotationX = Mathf.Clamp(_rotationX, -_extremeAngle, _extremeAngle);

        transform.Rotate(transform.up, _rotationY);
        _headTransform.localEulerAngles = Vector3.right * _rotationX;
    }
  
}
