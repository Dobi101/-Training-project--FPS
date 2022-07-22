using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class JumpPlatform : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private BoxCollider _triggerCollider;


    [Header("Customization")]
    [SerializeField]
    [Range(0f, 100f)]
    private float _jumpHeight = 10f;

    
    private void Start()
    {
        _triggerCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerMovement>() != null)
        {
            Rigidbody rigidbody = other.GetComponent<Rigidbody>();
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);

            float g = Mathf.Abs(Physics.gravity.y);
            float verticalVelocity = Mathf.Sqrt(2 * _jumpHeight * g);
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0, rigidbody.velocity.z);
            rigidbody.AddForce(transform.up * rigidbody.mass * verticalVelocity, ForceMode.Impulse);
        }
        
    }
}
