using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wallrun : MonoBehaviour
{

    public bool _isWallRunning { get; private set;}

    [SerializeField] 
    private float _wallDistance = .55f;
    [SerializeField] 
    private float _minimumJumpHeight = 1.5f;
    [SerializeField] 
    private float _wallRunGravity = 1;
    [SerializeField] 
    private float _wallRunJumpForce = 6;

    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }



}
