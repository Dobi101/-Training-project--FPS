using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/ FPS Input")]
public class FPSInput : MonoBehaviour
{
    [SerializeField] private float _speed = 6.0f;
    [SerializeField] private float _gravity = -9.8f;

    private float _deltaX;
    private float _deltaZ;
    private Vector3 _movement;

    private CharacterController _characterController;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        _deltaX = Input.GetAxis("Horizontal") * _speed;
        _deltaZ = Input.GetAxis("Vertical") * _speed;
        _movement = new Vector3(_deltaX, 0, _deltaZ);
        _movement = Vector3.ClampMagnitude(_movement, _speed);

        _movement.y = _gravity;

        _movement *= Time.deltaTime;
        _movement = transform.TransformDirection(_movement);
        _characterController.Move(_movement);
    }
}
